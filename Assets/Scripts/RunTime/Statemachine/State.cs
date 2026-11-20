using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine {

    public abstract class State : MonoBehaviour
    {
        public event Action onEnter;
        public event Action onExit;

        [SerializeReference] public List<Transition> transitions = new List<Transition>();

        protected Blackboard blackBoard;
        public virtual void Enter()
        {
            onEnter?.Invoke();
        }

        public State NextState()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].condition())
                {
                    return transitions[i].nextState;
                }
            }
            return null; // No valid transition found
        }

        public virtual void Exit()
        {
            onExit?.Invoke();
        }

        public virtual IEnumerator Step()
        {
            yield return null;
        }
    }
}


