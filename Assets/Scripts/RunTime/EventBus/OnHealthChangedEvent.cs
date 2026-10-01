using UnityEngine;

/// <summary>
/// An event to notify all subscribers that care about player health changing
/// </summary>
public class OnHealthChangedEvent : MonoBehaviour
{
    public readonly int healthChange;

    public OnHealthChangedEvent(int healthChange)
    {
        this.healthChange = healthChange;
    }
}
