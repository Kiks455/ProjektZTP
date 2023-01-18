using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZTP.Patterns.State
{
    public interface State
    {
        bool CheckAnswer(Word question, Word answer);
        void SetPoints(int points);
        int GetPoints();
    }
}
