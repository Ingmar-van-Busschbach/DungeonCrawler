using UnityEngine;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemData> startingItems;

        private List<Item> items;

        public ESortType sortType;
        public Item[] Items => items.ToArray();

        private void Awake()
        {
            GenerateInventory();
        }

        private void GenerateInventory()
        {
            items = new List<Item>();
            foreach (ItemData itemData in startingItems)
            {
                WeaponItemData weaponItemData = itemData as WeaponItemData;
                if (weaponItemData != null)
                {
                    items.Add(weaponItemData.CreateItem()); // Create an item from its data.
                }
            }
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public Item[] GetSortedItems()
        {
            return ItemSorter.SortItems(Items, sortType);
        }
    }
}