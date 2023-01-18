using ProjektZTP.Models;
using static ProjektZTP.Models.QuestionViewModels;

namespace ProjektZTP.Patterns.Iterator
{
    public interface InterfaceIterator
    {
        QuestionModel First();

        Word Next();

        bool IsDone();

        QuestionModel CurrentItem();
    }
}