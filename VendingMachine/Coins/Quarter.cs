namespace VendingMachine.Coins
{
    public class Quarter : Coin
    {
        public override int CentValue => 25;
        public override float Diameter => 24.26f;
        public override float Weight => 5.67f;
        public override string Name => Constants.QUARTER;
    }
}
