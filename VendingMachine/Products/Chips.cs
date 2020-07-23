using VendingMachine.Interfaces;

namespace VendingMachine.Products
{
    public class Chips : IProduct
    {
        public int Price => 50;
        public string Name => Constants.CHIPS;
    }
}
