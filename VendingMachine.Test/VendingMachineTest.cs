using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Interfaces;

namespace VendingMachine.Test
{
    [TestClass]
    public class VendingMachineTest : BaseVendingMachineTest
    {
        protected override IVendingMachine GetVendingMachineInstance()
        {
            VendingMachineFactory vendingMachineFactory = new VendingMachineFactory();
            return vendingMachineFactory.GetInstance();
        }
    }
}
