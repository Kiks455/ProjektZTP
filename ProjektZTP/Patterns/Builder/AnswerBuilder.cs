using ProjektZTP.Models;
using System.Collections.Generic;

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