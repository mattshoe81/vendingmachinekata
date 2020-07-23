using System.Collections.Generic;
using VendingMachine.Coins;

namespace VendingMachine.Interfaces
{
    public interface ICoinChanger
    {
        /// <summary>
        /// Given an amount of money to return and the coins available for making change, returns
        /// a list of the coins to return exact change to the user.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="availableChange"></param>
        /// <returns></returns>
        List<ICoin> MakeChange(int amount, Dictionary<string, Queue<ICoin>> availableChange);
    }
}
