using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using VendingMachine.Coins;
using VendingMachine.Interfaces;

namespace VendingMachine.Test
{
    [TestClass]
    public abstract class BaseVendingMachineTest
    {
        /// <summary>
        /// Design all tests against the interface, and allow a subclass
        /// to provide an implementation. This lets a standard suite of 
        /// tests be reused against many implementations.
        /// </summary>
        /// <returns></returns>
        protected abstract IVendingMachine GetVendingMachineInstance();

        [TestMethod]
        public void GetChips_ExactChange()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            string productName = Constants.CHIPS;

            IProduct product = machine.SelectProduct(productName);

            Assert.AreEqual(productName, product?.Name);
            Assert.AreEqual(0, machine.CoinReturn.Count);
        }

        [TestMethod]
        public void GetChips_InexactChange_SuperfluousCoin()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Nickel());
            string productName = Constants.CHIPS;

            IProduct product = machine.SelectProduct(productName);

            Assert.AreEqual(productName, product?.Name);
            Assert.AreEqual(1, machine.CoinReturn.Count);
            Assert.AreEqual(5, machine.CoinReturn[0].CentValue);
        }

        [TestMethod]
        public void GetCola_ExactChange()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            string productName = Constants.COLA;

            IProduct product = machine.SelectProduct(productName);

            Assert.AreEqual(productName, product?.Name);
            Assert.AreEqual(0, machine.CoinReturn.Count);
        }

        [TestMethod]
        public void GetCandy_ExactChange()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Nickel());
            string productName = Constants.CANDY;

            IProduct product = machine.SelectProduct(productName);

            Assert.AreEqual(productName, product?.Name);
            Assert.AreEqual(0, machine.CoinReturn.Count);
        }

        [TestMethod]
        public void MakeChange()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            CreateAvailableChange(machine);
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());

            machine.SelectProduct(Constants.CANDY);

            Assert.AreEqual(10, machine.CoinReturn.Sum(coin => coin.CentValue));
        }

        [TestMethod]
        public void Messages_RequireExactChange_AtStartup()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            Assert.AreEqual("EXACT CHANGE ONLY", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void Messages_RequireExactChange_AfterPurchase()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            string productName = Constants.CHIPS;

            machine.Messages.Clear();
            machine.SelectProduct(productName);
            // Clear the thank you message
            machine.Messages.Dequeue();

            Assert.AreEqual("EXACT CHANGE ONLY", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void Messages_ThankYouAfterPurchase()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.Messages.Clear();

            machine.SelectProduct(Constants.CHIPS);

            Assert.AreEqual("THANK YOU", machine.Messages.Dequeue());
            Assert.AreEqual("EXACT CHANGE ONLY", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void Messages_InsertCoin_AfterPurchase()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());

            machine.Messages.Clear();
            machine.SelectProduct(Constants.CHIPS);

            Assert.AreEqual("THANK YOU", machine.Messages.Dequeue());
            Assert.AreEqual("INSERT COIN", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void Messages_SoldOut()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            BuyChips(machine, 10);
            machine.InsertCoin(new Quarter());
            machine.InsertCoin(new Quarter());
            machine.Messages.Clear();

            machine.SelectProduct(Constants.CHIPS);

            Assert.AreEqual("SOLD OUT", machine.Messages.Dequeue());
            Assert.AreEqual("$0.50", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void InvalidCoin()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            ICoin coin = new InputCoin(42.42f, 0.42f);
            machine.Messages.Dequeue();

            machine.InsertCoin(coin);

            Assert.AreEqual("EXACT CHANGE ONLY", machine.Messages.Dequeue());
            Assert.AreEqual(1, machine.CoinReturn.Count);
            Assert.AreEqual(coin, machine.CoinReturn[0]);
        }

        [TestMethod]
        public void ImperfectNickelDiameterRecognized()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            ICoin coin = new InputCoin(21.15f, 5.0f);
            machine.Messages.Dequeue();

            machine.InsertCoin(coin);

            Assert.AreEqual("$0.05", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void ImperfectNickelWeightRecognized()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            ICoin coin = new InputCoin(21.21f, 5.05f);
            machine.Messages.Dequeue();

            machine.InsertCoin(coin);

            Assert.AreEqual("$0.05", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void ImperfectNickelDiameterAndWeightRecognized()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            ICoin coin = new InputCoin(21.25f, 5.05f);
            machine.Messages.Dequeue();

            machine.InsertCoin(coin);

            Assert.AreEqual("$0.05", machine.Messages.Dequeue());
        }

        [TestMethod]
        public void InsufficientFunds()
        {
            IVendingMachine machine = GetVendingMachineInstance();
            machine.InsertCoin(new Quarter());

            machine.Messages.Clear();
            machine.SelectProduct(Constants.CHIPS);

            Assert.AreEqual("PRICE $0.50", machine.Messages.Dequeue());
            Assert.AreEqual("$0.25", machine.Messages.Dequeue());
        }

        private void CreateAvailableChange(IVendingMachine machine)
        {
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Dime());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());
            machine.InsertCoin(new Nickel());
            string productName = Constants.CHIPS;

            IProduct product = machine.SelectProduct(productName);
        }

        private void BuyChips(IVendingMachine machine, int quantity, bool discardMessages = true)
        {
            for (int i = 0; i < quantity; i++)
            {
                machine.InsertCoin(new Quarter());
                machine.InsertCoin(new Quarter());
                machine.SelectProduct(Constants.CHIPS);
            }

            if (discardMessages)
                machine.Messages.Clear();
        }
    }
}
