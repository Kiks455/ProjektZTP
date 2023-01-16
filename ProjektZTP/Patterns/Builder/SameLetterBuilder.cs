using ProjektZTP.Data;
using ProjektZTP.Models;
using System.Collections.Generic;

namespace ProjektZTP.Patterns.Builder
{
    public class SameLetterBuilder : AnswerBuilder
    {
        private List<Word> words;
        private Word correctAnswer;
        private DbConnection db = DbConnection.GetDbConnection();

        public SameLetterBuilder(Word correctAnswer)
        {
            words = new List<Word>();
            this.correctAnswer = correctAnswer;
        }

        public void SetRandWord()
        {
            Word word = null;
            do
            {
                word = db.GetRandomWord();
            } while (word == correctAnswer);
            words.Add(word);
        }

        public void SetCorrectAnswer()
        {
            words.Add(correctAnswer);
        }

        public void SetSpecialWord()
        {
            Word word = null;
            do
            {
                word = db.GetSameLetterWord(correctAnswer);
            } while (word == correctAnswer);
            words.Add(word);
        }

        public List<Word> GetResult()
        {
            return words;
        }
    }
}