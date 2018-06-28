using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;
using Week6Lab_Identity.Models;
using Week6Lab_Identity.Models.ViewModels;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System;

namespace Week6Lab_Identity.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreContext _context;
        private readonly IConfiguration Configuration;

        public StoreController(SignInManager<ApplicationUser> signInMaganger,
            UserManager<ApplicationUser> userManager,
            StoreContext context,
            IConfiguration configuration
            )
        {
            _signInManager = signInMaganger;
            _userManager = userManager;
            _context = context;
            Configuration = configuration;
        }


        [HttpGet]
        public IActionResult Index ()
        {
            return View(_context.Words.Where(x => x.Type == WordType.General).ToList());
        }

        [Authorize(Policy = "Educational")]
        [HttpGet]
        public IActionResult Student ()
        {
            return View(_context.Words.Where(x => x.Type == WordType.Education).ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        public async Task <ViewResult> CheckoutSuccess()
        {
            //Having successfully checked out, clear the basket
            var user = await _userManager.GetUserAsync(User);
            user.BasketId++;
            await _userManager.UpdateAsync(user);
            //Show success
            return View();
        }

        [HttpGet]
        public ViewResult TransactionError()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(int Id)
        {
            var user = await _userManager.GetUserAsync(User);
            int quantAdd = 1; //TODO add quantity feature
            if (_context.Basket.FirstOrDefault(x => x.ItemId == Id) != null)
            {
                _context.Basket.FirstOrDefault(x => x.ItemId == Id).ItemQuantity += quantAdd;
            }
            else
            {
                BasketItem b = new BasketItem()
                {
                    UserBasketNum = user.BasketId,
                    UserKey = user.Id,
                    ItemId = Id,
                    ItemQuantity = quantAdd
                };
                await _context.Basket.AddAsync(b);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Removes item from cart for the current user based on item id
        /// </summary>
        /// <param name="Id">id of item to be removed</param>
        /// <param name="all"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCartAsync (int Id, string source, bool all = true)
        {
            //TODO add button to use the all bool to remove one at a time (Default all)
            var user = await _userManager.GetUserAsync(User);
            var userBasket = _context.Basket.Where(x => x.UserBasketNum == user.BasketId && x.UserKey == user.Id);
            var item = userBasket.FirstOrDefault(x => x.ItemId == Id);
            if ( item != null)
            {
                item.ItemQuantity = (item.ItemQuantity < 1 || all ) ? 0 : item.ItemQuantity - 1;
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(source);
        }

        /// <summary>
        /// This method uses Authorize Net example code for their NuGet package, with minor tweaks
        /// To make it work for this application
        /// </summary>
        /// <param name="cardInformation">Card information passed into the form</param>
        /// <returns>Redirect to success or fail page, depending on whether transaction succeeds or fails</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(Charge cardInformation)
        {
            //Get Secrets
            string ApiLoginID = "2Ee3R8Uc2h";
            string ApiTransactionKey = "4b6FW2kpsYXY3374";// Configuration["AuthTransactionKey"];

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardInformation.Number,
                expirationDate = cardInformation.Experation
            };

            var pay = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = 123.45m, //TODO once basket is working, pull from basket
                payment = pay
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    try
                    {
                        ViewData["FourDigits"] = int.Parse(cardInformation.Number) % 10000;
                    }
                    catch (Exception e)
                    {
                        ViewData["ErrorMessage"] = e.Message;
                        //TODO log error
                    }
                    return await CheckoutSuccess();
                }
                ViewData["ErrorMessage"] = "null transaction response";
                return RedirectToAction("TransactionError");
            }
            else
            {
                ViewData["ErrorMessage"] = "Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text;

                if (response.transactionResponse != null)
                {
                    ViewData["ErrorMessage"] = "Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText;
                }

                return RedirectToAction("TransactionError");
            }
        }
    }
}
