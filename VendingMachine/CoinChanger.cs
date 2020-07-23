using System;
using System.Collections.Generic;
using VendingMachine.Coins;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    internal class CoinChanger : ICoinChanger
    {
        public List<ICoin> MakeChange(int amount, Dictionary<string, Queue<ICoin>> availableChange)
        {
            Queue<ICoin> quarters = availableChange[Constants.QUARTER];
            Queue<ICoin> dimes = availableChange[Constants.DIME];
            Queue<ICoin> nickels = availableChange[Constants.NICKEL];
            List<ICoin> coinsReturned = new List<ICoin>();
            /*
             * Try to return the largest coins available first.
             */
            if (amount >= 25)
            {
                while (quarters.Count > 0 && amount >= 25)
                {
                    coinsReturned.Add(quarters.Dequeue());
                    amount -= 25;
                }
            }
            if (amount >= 10)
            {
                while (dimes.Count > 0 && amount >= 10)
                {
                    coinsReturned.Add(dimes.Dequeue());
                    amount -= 10;
                }
            }
            if (amount > 0)
            {
                while (nickels.Count > 0 && amount > 0)
                {
                    coinsReturned.Add(nickels.Dequeue());
                    amount -= 5;
                }
            }

            if (amount != 0)
                throw new Exception("Dang it.");

            return coinsReturned;
        }
    }
}
