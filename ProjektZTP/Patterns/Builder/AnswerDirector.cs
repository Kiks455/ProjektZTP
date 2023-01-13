namespace ProjektZTP.Patterns.Builder
{
    public class AnswerDirector
    {
        public void Construct(AnswerBuilder answerBuilder, int level)
        {
            answerBuilder.SetCorrectAnswer();
            int quantity = GetQuantity(level);
            for (int i = 0; i < quantity - 1; i++)
            {
                if (level == 0)
                {
                    answerBuilder.SetSpecialWord();
                }
                else if (level == 1)
                {
                    if (i < 2)
                    {
                        answerBuilder.SetSpecialWord();
                    }
                    else
                        answerBuilder.SetRandWord();
                }
            }
        }

        private int GetQuantity(int level)
        {
            int quantity;
            switch (level)
            {
                case 0:
                    {
                        quantity = 3;
                        break;
                    }
                case 1:
                    {
                        quantity = 5;
                        break;
                    }
                default:
                    {
                        quantity = 0;
                        break;
                    }
            }
            return quantity;
        }
    }
}