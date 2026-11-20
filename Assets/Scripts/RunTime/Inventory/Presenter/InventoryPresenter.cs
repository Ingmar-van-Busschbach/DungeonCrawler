using UnityEngine;

namespace Inventory
{
    public abstract class InventoryPresenter : MonoBehaviour
    {
        [SerializeField] protected InventoryContainer inventory;

        protected abstract void PresentInventory();
        public void ChangeInventorySorting(ESortType sortType)
        {
            inventory.sortType = sortType;
            PresentInventory();
        }
    }
}