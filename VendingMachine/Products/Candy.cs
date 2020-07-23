using VendingMachine.Interfaces;

namespace VendingMachine.Products
{
    public class Candy : IProduct
    {
        public int Price => 65;
        public string Name => Constants.CANDY;
    }
}
