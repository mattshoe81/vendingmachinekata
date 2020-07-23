using System;

namespace VendingMachine.Coins
{
    public abstract class Coin : ICoin
    {
        protected const float DIAMETER_TOLERANCE = 0.1f;
        protected const float WEIGHT_TOLERANCE = 0.1f;
        public abstract int CentValue { get; }
        public abstract float Diameter { get; }
        public abstract float Weight { get; }
        public abstract string Name { get; }

        /// <summary>
        /// Leverage the Equals method override to provide the comparator for the logic of
        /// determining if two coins are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool result;

            if (obj == null || !(obj is Coin))
                result = false;
            if (object.ReferenceEquals(this, obj))
                result = true;
            else
            {
                Coin coin = obj as Coin;
                bool withinSizeTolerance = Math.Abs(Diameter - coin.Diameter) <= DIAMETER_TOLERANCE;
                bool withinWeightTolerance = Math.Abs(Weight - coin.Weight) <= WEIGHT_TOLERANCE;

                result = withinSizeTolerance && withinWeightTolerance;
            }

            return result;
        }

        public override int GetHashCode()
        {
            if (Name == null)
                return CentValue.GetHashCode();
            else
                return Name.GetHashCode();
        }
    }
}
