using VendingMachine.Coins;

namespace VendingMachine.Interfaces
{
    public interface ICoinClassifier
    {
        /// <summary>
        /// Given a coin, determine its type based on physical characteristics, and 
        /// return the known coin.
        /// </summary>
        /// <param name="inputCoin"></param>
        /// <returns></returns>
        ICoin ClassifyCoin(ICoin inputCoin);
    }
}
