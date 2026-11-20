using Damage;
using UnityEngine;

namespace Inventory
{
    public class WeaponItem : Item
    {
        public DamageStruct damageSettings { get; private set; }
        public WeaponItem(WeaponItemData itemData)
        {
            id = itemData.id;
            itemName = itemData.itemName;
            itemIcon = itemData.itemIcon;
            damageSettings = itemData.damageSettings;
        }
    }
}