namespace VendingMachine.Coins
{
    public class Nickel : Coin
    {
        public override int CentValue => 5;
        public override float Diameter => 21.21f;
        public override float Weight => 5.0f;
        public override string Name => Constants.NICKEL;
    }
}
