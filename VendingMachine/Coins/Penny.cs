namespace VendingMachine.Coins
{
    public class Penny : Coin
    {
        public override int CentValue => 1;
        public override float Diameter => 19.05f;
        public override float Weight => 2.5f;
        public override string Name => Constants.PENNY;
    }
}
