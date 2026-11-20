using System.Linq;

namespace Inventory
{
    public static class AlphabeticalSorter
    {
        public static Item[] Sort(Item[] items)
        {
            return items.OrderBy(items => items.itemName).ToArray();
        }
    }
}