namespace VendingMachine.Coins
{
    public class InputCoin : Coin
    {
        public override int CentValue { get; }
        public override float Diameter { get; }
        public override float Weight { get; }
        public override string Name { get; } = "Unknown";

        public InputCoin() { }

        public InputCoin(float diameter, float weight)
        {
            Diameter = diameter;
            Weight = weight;
        }
    }
}
