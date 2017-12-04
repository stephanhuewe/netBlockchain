using System.Collections.Generic;
using System.Linq;
using Huestel.Blockchain.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huestel.Blockchain.Tests
{
    [TestClass]
    public class MinerTest
    {
        [TestMethod]
        public void CheckBlockHashTest()
        {
            Example.Blockchain chain = new Example.Blockchain();
            BlockchainHelper helper = new BlockchainHelper();

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -3);
            transactions.Add("Bob", 3);

            Block newBlock = helper.CreateBlock(transactions, chain);

            Miner miner = new Miner();
            bool result = miner.CheckBlockHash(newBlock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckBlockTest()
        {
            Example.Blockchain chain = new Example.Blockchain();
            BlockchainHelper helper = new BlockchainHelper();

            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -3);
            transactions.Add("Bob", 3);

            Block newBlock = helper.CreateBlock(transactions, chain);
            Block parentBlock = chain.Chain[0];
            
            Miner miner = new Miner();
            List<string> errors;
            miner.CheckBlock(newBlock, parentBlock, states, out errors);

            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void CheckBlockChainTest()
        {
            DataHelper helper = new DataHelper();
            Example.Blockchain blockchain = helper.GetSampleChain();

            Miner miner = new Miner();
            Dictionary<string, decimal> result = miner.CheckBlockchain(blockchain);

            Assert.AreEqual(result.Values.Sum(), 200);
        }
    }
}
