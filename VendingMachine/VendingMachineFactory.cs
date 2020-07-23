using System;
using System.Collections.Generic;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    public class VendingMachineFactory
    {
        private Dictionary<Type, object> bindings;

        public VendingMachineFactory()
        {
            bindings = new Dictionary<Type, object>();
            bindings.Add(typeof(ICoinClassifier), new CoinClassifier());
            bindings.Add(typeof(IInventoryManager), new InventoryManager());
            bindings.Add(typeof(ICoinChanger), new CoinChanger());
        }

        /// <summary>
        /// This allows us to keep all implementations hidden (internal) and 
        /// only expose interfaces to use this class library's functionality.
        /// </summary>
        /// <returns></returns>
        public IVendingMachine GetInstance()
        {
            return new VendingMachine(
                Get<ICoinClassifier>(),
                Get<IInventoryManager>(),
                Get<ICoinChanger>()
            );
        }

        /// <summary>
        /// Bind an alternate implementation to the specified interface. All new injections of
        /// the specified interface will use the given implementation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        public void Bind<TInterface, TImplementation>(TImplementation implementation) 
            where TInterface : class 
            where TImplementation : TInterface
        {
            if (bindings.ContainsKey(typeof(TInterface)))
                bindings.Remove(typeof(TInterface));

            bindings.Add(typeof(TInterface), implementation);
        }

        private T Get<T>() where T : class
        {
            if (bindings.ContainsKey(typeof(T)))
                return bindings[typeof(T)] as T;
            else
                return null;
        }
    }
}
