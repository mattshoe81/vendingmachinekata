using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Coins;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    internal class VendingMachine : IVendingMachine
    {
        // Behavioral Dependencies
        private ICoinClassifier coinProcessor;
        private IInventoryManager inventoryManager;
        private ICoinChanger coinChanger;

        // State variables
        private Dictionary<string, Queue<ICoin>> availableChange;
        private List<ICoin> currentTransactionCoins;
        private int currentBalance => currentTransactionCoins.Sum(coin => coin.CentValue);

        public Queue<string> Messages { get; set; }

        public List<ICoin> CoinReturn { get; set; }

        public List<string> AvailableProducts => inventoryManager.AvailableProducts;

        public VendingMachine(ICoinClassifier coinProcessor, IInventoryManager inventoryManager, ICoinChanger coinChanger)
        {
            this.coinProcessor = coinProcessor;
            this.inventoryManager = inventoryManager;
            this.coinChanger = coinChanger;

            currentTransactionCoins = new List<ICoin>();
            CoinReturn = new List<ICoin>();
            availableChange = new Dictionary<string, Queue<ICoin>>();
            availableChange[Constants.QUARTER] = new Queue<ICoin>();
            availableChange[Constants.DIME] = new Queue<ICoin>();
            availableChange[Constants.NICKEL] = new Queue<ICoin>();

            Messages = new Queue<string>();
            DisplayMessage("EXACT CHANGE ONLY");
        }
        public void InsertCoin(ICoin insertedCoin)
        {
            ICoin actualCoin = coinProcessor.ClassifyCoin(insertedCoin);

            if (actualCoin == null)
                CoinReturn.Add(insertedCoin);
            else
                currentTransactionCoins.Add(actualCoin);

            PromptForCoins();
        }

        public IProduct SelectProduct(string productName)
        {
            IProduct result = null;
            if (AvailableProducts.Contains(productName))
            {
                int itemPrice = inventoryManager.GetPrice(productName);
                if (itemPrice > currentBalance)
                    HandleInsufficientFunds(itemPrice);
                else
                {
                    result = inventoryManager.Dispense(productName);
                    if (result == null)
                        HandleSoldOut();
                    else
                        HandleValidSelection(result);
                }
            }
            PromptForCoins();

            return result;
        }
        public void ReturnCoins()
        {
            CoinReturn.AddRange(currentTransactionCoins);
            currentTransactionCoins = new List<ICoin>();
            PromptForCoins();
        }

        private void PromptForCoins()
        {
            if (currentTransactionCoins.Count == 0)
            {
                if (AllowInexactChange())
                    DisplayMessage("INSERT COIN");
                else
                    DisplayMessage("EXACT CHANGE ONLY");
            }
            else
            {
                ShowBalance();
            }
        }

        private bool AllowInexactChange()
        {
            Queue<ICoin> dimes = availableChange[Constants.DIME];
            Queue<ICoin> nickels = availableChange[Constants.NICKEL];

            bool atLeastOneDime = dimes != null && dimes.Count > 0;
            bool atLeastTwoNickels = nickels != null && nickels.Count >= 2;
            bool atLeastFourNickels = nickels != null && nickels.Count >= 4;

            return (atLeastOneDime && atLeastTwoNickels) || atLeastFourNickels;
        }

        private void InternalizeCoin(ICoin coin)
        {
            if (!availableChange.ContainsKey(coin.Name))
                availableChange[coin.Name] = new Queue<ICoin>();

            availableChange[coin.Name].Enqueue(coin);
        }

        private void HandleInsufficientFunds(int price)
        {
            DisplayMessage(String.Format("PRICE {0:C2}", price / 100d));
        }

        private void HandleSoldOut()
        {
            DisplayMessage("SOLD OUT");
            if (currentTransactionCoins.Count > 0)
                ShowBalance();
        }

        private void ShowBalance()
        {
            DisplayMessage(String.Format("{0:C2}", currentBalance / 100d));
        }

        private void HandleValidSelection(IProduct product)
        {
            int amountToReturn = currentBalance - product.Price;

            currentTransactionCoins.ForEach(coin => InternalizeCoin(coin));
            List<ICoin> change = coinChanger.MakeChange(amountToReturn, availableChange);
            CoinReturn.AddRange(change);

            currentTransactionCoins = new List<ICoin>();
            DisplayMessage("THANK YOU");
        }

        private void DisplayMessage(string message)
        {
            // No point adding the same message twice.
            if (Messages.Count == 0 || Messages.Last() != message)
                Messages.Enqueue(message);
        }
    }
}
