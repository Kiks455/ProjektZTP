using System.Collections.Generic;

namespace ProjektZTP.Models
{
    public class QuestionViewModels
    {
        public class QuestionModel
        {
            public Word Word { get; set; }
            public List<Word> Answers { get; set; }
            public int QuestionNumber { get; set; }
        }

        public class AnsweredQuestionModel
        {
            public Word Word { get; set; }
            public List<Word> Answers { get; set; }
            public int AnswerId { get; set; }
            public int QuestionNumber { get; set; }
            public string Mode { get; set; }
            public string Lang { get; set; }
        }

        public class QuestionHardModel
        {
            public Word Word { get; set; }
            public int QuestionNumber { get; set; }
            public string Answer { get; set; }
            public string Mode { get; set; }
            public string Lang { get; set; }
        }
    }
}