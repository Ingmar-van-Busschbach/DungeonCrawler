/// <summary>
/// An event to notify all subscribers that care about xp
/// </summary>

namespace EventBus
{
    public class GetXPEvent : Event
    {
        public readonly int xp;
        public GetXPEvent(int xp)
        {
            this.xp = xp;
        }
    }
}