using UnityEngine;

namespace Inventory
{
    public class ListInventoryPresenter : InventoryPresenter
    {
        [SerializeField] private ItemPresenter itemPresenterPrefab;
        [SerializeField] private Transform listParent;

        private void Start()
        {
            PresentInventory();
        }
        protected override void PresentInventory()
        {
            for(int i = 0; i < listParent.childCount; i++)
            {
                Destroy(listParent.GetChild(0).gameObject);
            }
            Item[] items = inventory.GetSortedItems();
            for (int i = 0; i < items.Length; i++)
            {
                ItemPresenter itemPresenter = Instantiate(itemPresenterPrefab, listParent);
                itemPresenter.PresentItem(items[i]);
            }
        }
    }
}