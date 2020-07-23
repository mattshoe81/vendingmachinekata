using System.Collections.Generic;
using System.Linq;
using VendingMachine.Interfaces;
using VendingMachine.Products;

namespace VendingMachine
{
    internal class InventoryManager : IInventoryManager
    {
        private const int DEFAULT_INVENTORY_QUANTITY = 10;
        private List<ProductInventory> inventory;
        private Dictionary<string, IProduct> products;

        public List<string> AvailableProducts => products.Keys.ToList();

        public InventoryManager()
        {
            inventory = new List<ProductInventory>
            {
                new ProductInventory(new Chips(), DEFAULT_INVENTORY_QUANTITY),
                new ProductInventory(new Cola(), DEFAULT_INVENTORY_QUANTITY),
                new ProductInventory(new Candy(), DEFAULT_INVENTORY_QUANTITY)
            };

            products = new Dictionary<string, IProduct>();
            inventory.ForEach(inventoryItem => products.Add(inventoryItem.Product.Name, inventoryItem.Product));
        }

        public IProduct Dispense(string product)
        {
            IProduct result = null;
            if (product != null)
            {
                ProductInventory productInventory = inventory.Where(item => item.Product.Name == product).FirstOrDefault();
                if (productInventory != null && productInventory.QuantityInStock > 0)
                {
                    productInventory.QuantityInStock--;
                    result = productInventory.Product;
                }
            }

            return result;
        }

        public int GetPrice(string productName)
        {
            int price = -1;
            if (products.ContainsKey(productName))
                price = products[productName].Price;

            return price;
        }
    }
}
