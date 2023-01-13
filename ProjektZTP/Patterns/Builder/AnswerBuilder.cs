using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektZTP.Patterns.Builder
{
    public interface AnswerBuilder
    {
        void SetRandWord();
        void SetCorrectAnswer();
        void SetSpecialWord();
        List<Word> GetResult();
    }
}