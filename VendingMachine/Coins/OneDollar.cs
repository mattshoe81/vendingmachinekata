namespace VendingMachine.Coins
{
    public class OneDollar : Coin
    {
        public override int CentValue => 100;
        public override float Diameter => 26.49f;
        public override float Weight => 8.1f;
        public override string Name => Constants.DIME;
    }
}
