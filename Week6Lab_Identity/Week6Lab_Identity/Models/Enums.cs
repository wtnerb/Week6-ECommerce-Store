using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models
{
    public enum Region
    {
        Seattle,
        Tacoma,
        Spokane,
        Bellingham,
        Pullman,
        Everett
    }

    public enum POS
    {
        verb,
        noun,
        adverb,
        pronoun,
        adjective,
        preposition
    }

    //Currently 2018-05-10 only General and Education are used
    public enum WordType
    {
        General,
        Education,
        Government,
        Restricted
    }
}
