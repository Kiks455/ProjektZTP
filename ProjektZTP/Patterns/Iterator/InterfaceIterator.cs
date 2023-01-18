using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
