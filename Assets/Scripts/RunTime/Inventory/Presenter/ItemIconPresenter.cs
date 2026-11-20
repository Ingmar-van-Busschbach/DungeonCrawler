using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemIconPresenter : ItemPresenter
    {
        private Image image;
        public void Awake()
        {
            image = GetComponent<Image>();
        }
        public override void PresentItem(Item item)
        {
            image.sprite = item.itemIcon;
        }
    }
}


