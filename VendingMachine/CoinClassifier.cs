using System.Linq;
using VendingMachine.Coins;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    internal class CoinClassifier : ICoinClassifier
    {
        private static readonly ICoin[] validCoins;

        static CoinClassifier()
        {
            validCoins = new ICoin[]
            {
                new Nickel(),
                new Dime(),
                new Quarter()
            };
        }

        /// <summary>
        /// Connverts the given coin to a known valid coin value, based on its
        /// size and weight. If no valid coin is found, null is returned.
        /// </summary>
        /// <param name="inputCoin"></param>
        /// <returns></returns>
        public ICoin ClassifyCoin(ICoin inputCoin)
        {
            if (inputCoin == null)
                return null;

            ICoin actualCoin = validCoins.Where(validCoin => validCoin.Equals(inputCoin)).FirstOrDefault();
            return actualCoin;
        }
    }
}
