using System;

namespace StateMachine
{
    [Serializable]
    public class Transition
    {
        public Func<bool> condition;
        public State nextState;
        public Transition(Func<bool> pCondition, State pNextState)
        {
            condition = pCondition;
            nextState = pNextState;
        }

    }
}

