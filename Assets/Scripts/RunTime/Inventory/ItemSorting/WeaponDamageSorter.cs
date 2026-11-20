using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public static class WeaponDamageSorter
    {
        public static Item[] Sort(Item[] items)
        {
            List<WeaponItem> weaponItems = new();
            List<Item> remainingItems = new();
            foreach (Item item in items)
            {
                WeaponItem weaponItem = item as WeaponItem;
                if (weaponItem != null)
                {
                    weaponItems.Add(weaponItem);
                }
                else
                {
                    remainingItems.Add(item);
                }
            }
            weaponItems = weaponItems.OrderBy(weaponItems => weaponItems.damageSettings.BaseDamage()).ToList();
            Item[] result = new Item[weaponItems.Count + remainingItems.Count];
            weaponItems.ToArray().CopyTo(result, 0);
            remainingItems.ToArray().CopyTo(result, weaponItems.Count);
            return result;
        }
    }
}