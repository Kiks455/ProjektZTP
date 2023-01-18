using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektZTP.Patterns.State
{
    public enum StateMode
    {
        Learning,
        Test
    }

    public class Context
    {
        private State _state;

        public Context()
        {

        }

        public void ChangeState(StateMode state)
        {
            switch(state)
            {
                case StateMode.Learning:
                    {
                        _state = new LearningState();
                        break;
                    }
                case StateMode.Test:
                    {
                        _state = new TestState();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "This state is not implemented");
            }
        }

        public State GetState()
        {
            return _state;
        }
    }
}