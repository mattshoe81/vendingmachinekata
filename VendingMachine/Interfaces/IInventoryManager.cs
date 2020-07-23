using System.Collections.Generic;
using VendingMachine.Products;

namespace VendingMachine.Interfaces
{
    public interface IInventoryManager
    {
        /// <summary>
        /// The names of all produccts available for purchase.
        /// </summary>
        List<string> AvailableProducts { get; }

        /// <summary>
        /// Dispense the specified product if in stock.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        IProduct Dispense(string product);

        /// <summary>
        /// Get the price of the specified product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int GetPrice(string product);
    }
}
