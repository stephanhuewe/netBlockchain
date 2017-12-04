using System.Collections.Generic;
using Huestel.Blockchain.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huestel.Blockchain.Tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionSerializeTest()
        {
            TransactionPart partBob = new TransactionPart("Bob", 100);
            TransactionPart partAlice = new TransactionPart("Alice", 100);

            Transaction test = new Transaction(partBob, partAlice);
            string json = test.ToString();

            string expected = @"{""Currentransaction"":{""Item1"":{""Person"":""Bob"",""Amount"":100.0},""Item2"":{""Person"":""Alice"",""Amount"":100.0}}}";
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void BasicTransactionTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -3);
            transactions.Add("Bob", 3);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BasicTransactionFailTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -3);
            transactions.Add("Bob", 4);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TransactionOverpayFailTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -6);
            transactions.Add("Bob", 6);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TransactionNewUserTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -2);
            transactions.Add("Peter", 2);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TransactionNewUserOverpayTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -6);
            transactions.Add("Peter", 6);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsFalse(result);
        }

    }
}
