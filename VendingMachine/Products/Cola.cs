using VendingMachine.Interfaces;

namespace VendingMachine.Products
{
    public class Cola : IProduct
    {
        public int Price => 100;
        public string Name => Constants.COLA;
    }
}
