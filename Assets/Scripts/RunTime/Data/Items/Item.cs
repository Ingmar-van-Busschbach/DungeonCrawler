using UnityEngine;

namespace Inventory
{
    public abstract class Item
    {
        public string id { get; protected set; }
        public string itemName { get; protected set; }
        public Sprite itemIcon { get; protected set; }
    }
}
