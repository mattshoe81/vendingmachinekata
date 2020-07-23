using VendingMachine.Interfaces;

namespace VendingMachine.Products
{
    public abstract class Product : IProduct
    {
        public abstract int Price { get; }
        public abstract string Name { get; }

        public override bool Equals(object obj)
        {
            bool result;

            if (obj == null || !(obj is Product))
                result = false;
            if (object.ReferenceEquals(this, obj))
                result = true;
            else
                result = Name == (obj as Product).Name;

            return result;
        }

        public override int GetHashCode()
        {
            if (Name == null)
                return Price.GetHashCode();
            else
                return Name.GetHashCode();
        }
    }
}
