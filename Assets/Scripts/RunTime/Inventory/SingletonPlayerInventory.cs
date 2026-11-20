using UnityEngine;

namespace Inventory
{
    public class SingletonPlayerInventory : MonoBehaviour
    {
        public InventoryContainer inventory;

        public static SingletonPlayerInventory Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (inventory == null)
            {
                if (!TryGetComponent(out inventory))
                {
                    inventory = gameObject.AddComponent<InventoryContainer>();
                }
            }

        }
    }
}