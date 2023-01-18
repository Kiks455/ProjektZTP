using ProjektZTP.Models;
using System.Collections.Generic;

namespace ProjektZTP.Patterns.Builder
{
    public interface AnswerBuilder
    {
        void FetchRandWord();

        void SetCorrectAnswer();

        void FetchSpecialWord();

        List<Word> GetResult();
    }
}