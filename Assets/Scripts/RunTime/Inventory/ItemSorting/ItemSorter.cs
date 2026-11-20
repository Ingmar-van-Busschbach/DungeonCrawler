using UnityEngine;

namespace Inventory
{
    public static class ItemSorter
    {
        public static Item[] SortItems(Item[] items, ESortType sortType)
        {
            switch (sortType)
            {
                case ESortType.Alphabetical:
                    return AlphabeticalSorter.Sort(items);
                case ESortType.Damage:
                    return WeaponDamageSorter.Sort(items);
                default:
                    return items;
            }
            
        }
    }
}