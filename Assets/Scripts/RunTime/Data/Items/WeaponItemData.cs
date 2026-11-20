using Damage;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "Scriptable Objects/WeaponItemData")]
    public class WeaponItemData : ItemData
    {
        public DamageStruct damageSettings;

        public WeaponItem CreateItem()
        {
            return new WeaponItem(this);
        }
    }
}