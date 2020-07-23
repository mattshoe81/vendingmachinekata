using VendingMachine.Interfaces;

namespace VendingMachine.Products
{
    public class ProductInventory
    {
        public int QuantityInStock { get; set; }
        public IProduct Product { get; set; }

        public ProductInventory(IProduct product, int quantity)
        {
            Product = product;
            QuantityInStock = quantity;
        }
    }
}
