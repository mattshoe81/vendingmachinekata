namespace VendingMachine.Interfaces
{
    public interface IProduct
    {
        /// <summary>
        /// Τhe product's price in cents.
        /// </summary>
        int Price { get; }

        /// <summary>
        /// The display name of this product.
        /// </summary>
        string Name { get; }
    }
}
