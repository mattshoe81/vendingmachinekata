namespace VendingMachine.Coins
{
    public interface ICoin
    {
        /// <summary>
        /// The integer cent value of this coin.
        /// </summary>
        int CentValue { get; }

        /// <summary>
        /// Diameter of the coin in millimeters.
        /// </summary>
        float Diameter { get; }

        /// <summary>
        /// Weight of the coin in grams.
        /// </summary>
        float Weight { get; }

        /// <summary>
        /// The "user-friendly" common name of this type of coin.
        /// </summary>
        string Name { get; }
    }
}
