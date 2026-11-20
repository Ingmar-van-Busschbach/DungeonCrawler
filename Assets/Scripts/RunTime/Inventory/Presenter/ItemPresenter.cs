using UnityEngine;

namespace Inventory
{
    public abstract class ItemPresenter : MonoBehaviour
    {
        public abstract void PresentItem(Item item);
    }
}