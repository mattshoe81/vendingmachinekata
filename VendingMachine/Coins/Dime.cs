namespace VendingMachine.Coins
{
    public class Dime : Coin
    {
        public override int CentValue => 10;
        public override float Diameter => 17.91f;
        public override float Weight => 2.268f;
        public override string Name => Constants.DIME;
    }
}
