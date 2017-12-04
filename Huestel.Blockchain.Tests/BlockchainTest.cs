using System;
using System.Collections.Generic;
using Huestel.Blockchain.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huestel.Blockchain.Tests
{
    [TestClass]
    public class BlockchainTest
    {
        [TestMethod]
        public void CreateFirstBlockTest()
        {
            Example.Blockchain chain = new Example.Blockchain();
            BlockchainHelper helper = new BlockchainHelper();

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -3);
            transactions.Add("Bob", 3);

            Block newBlock =  helper.CreateBlock(transactions, chain);

            Assert.AreEqual(newBlock.Content.BlockNumber, 1);
            Assert.AreEqual(newBlock.Content.NumberOfTransactions, 2);
            Assert.AreNotEqual(newBlock.Content.ParentHash, String.Empty);
        }

        [TestMethod]
        public void ChainUniqueNumberTest()
        {
            DataHelper helper = new DataHelper();
            Example.Blockchain blockchain = helper.GetSampleChain();

            int lastnumber = 0;
            foreach (Block chain in blockchain.Chain)
            {
                Assert.AreEqual(chain.Content.BlockNumber, lastnumber);
                lastnumber++;
            }
        }

        [TestMethod]
        public void ChainHashTest()
        {
            DataHelper helper = new DataHelper();
            Example.Blockchain blockchain = helper.GetSampleChain();

            string currentHash = String.Empty;
            foreach (Block chain in blockchain.Chain)
            {
                Assert.AreEqual(chain.Content.ParentHash, currentHash);
                currentHash = Hash.CreateHash(chain.Content.ToString());
            }
        }

        
    }
}
