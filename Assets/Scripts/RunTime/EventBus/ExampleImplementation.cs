using UnityEngine;

namespace EventBus
{
    public class ExampleImplementationPublisher : MonoBehaviour
    {
        public void ExamplePublisherFunction(int droppedXP)
        {
            EventBus<GetXPEvent>.Publish(new GetXPEvent(droppedXP));
        }
    }

    public class ExampleImplementationSubscriber : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus<GetXPEvent>.OnEvent += ExampleSubscribedFunction;
        }
        private void OnDisable()
        {
            EventBus<GetXPEvent>.OnEvent -= ExampleSubscribedFunction;
        }
        public void ExampleSubscribedFunction(GetXPEvent xpEvent)
        {
            int newXP = xpEvent.xp;
        }
    }
}