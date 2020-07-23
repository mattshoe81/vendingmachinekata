using VendingMachine.Coins;

namespace VendingMachine
{
    public class Constants
    {
        public const string NICKEL = "Nickel";
        public const string DIME = "Dime";
        public const string QUARTER = "Quarter";
        public const string PENNY = "Penny";
        public const string HALF_DOLLAR = "Half Dollar";
        public const string ONE_DOLLAR = "One Dollar";

        public const string CHIPS = "CHIPS";
        public const string CANDY = "CANDY";
        public const string COLA = "COLA";

        public static readonly ICoin[] ALL_COINS = new ICoin[]
        {
            new Nickel(),
            new Dime(),
            new Quarter(),
            new Penny(),
            new HalfDollar(),
            new OneDollar()
        };
    }
}
