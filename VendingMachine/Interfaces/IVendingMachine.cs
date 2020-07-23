using System.Collections.Generic;
using VendingMachine.Coins;

namespace VendingMachine.Interfaces
{
    public interface IVendingMachine
    {
        /// <summary>
        /// A queue of messages used to display information to the user.
        /// </summary>
        List<string> AvailableProducts { get; }

        /// <summary>
        /// A queue of messages to display to the user.
        /// </summary>
        Queue<string> Messages { get; set; }

        /// <summary>
        /// The coins that have been returned to the user.
        /// </summary>
        List<ICoin> CoinReturn { get; set; }

        /// <summary>
        /// Insert a coin into the machine.
        /// </summary>
        /// <param name="insertedCoin"></param>
        void InsertCoin(ICoin insertedCoin);

        /// <summary>
        /// Return all coins in this transaction to the CoinReturn.
        /// </summary>
        void ReturnCoins();

        /// <summary>
        /// Select a product for purchase.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        IProduct SelectProduct(string productName);
    }
}