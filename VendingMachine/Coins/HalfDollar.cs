namespace VendingMachine.Coins
{
    public class HalfDollar : Coin
    {
        public override int CentValue => 50;
        public override float Diameter => 30.61f;
        public override float Weight => 11.34f;
        public override string Name => Constants.HALF_DOLLAR;
    }
}
