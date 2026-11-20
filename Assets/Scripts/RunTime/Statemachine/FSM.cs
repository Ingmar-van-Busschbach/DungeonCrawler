using System.Collections;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// Abstract base class for building finite state machines (FSMs).
    /// Inherits from State, allowing FSMs to be used as state themselves to build
    /// sub-state machines.
    /// </summary>
    public abstract class FSM : State
    {
        protected State currentState;

        public override IEnumerator Step()
        {
            base.Step();
            yield return StartCoroutine(currentState.Step());
            if (currentState.NextState() != null)
            {
                //Cache the next state, because after currentState.Exit, calling
                //currentState.NextState again might return null because of change
                //of context.
                State nextState = currentState.NextState();
                currentState.Exit();
                currentState = nextState;
                currentState.Enter();
            }
            yield return null;
        }
    }
}
