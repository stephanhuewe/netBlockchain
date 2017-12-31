using System;
using System.Collections.Generic;
using Huestel.Blockchain.Example;

namespace Huestel.Blockchain.Tests
{
    internal class DataHelper
    {
        private readonly Random _rand;
        public DataHelper()
        {
             _rand = new Random();
        }
        public Transaction CreateSampleTransaction()
        {
            int sign = 1;
            if (_rand.Next(0, 2) == 0)
            {
                sign = -1;
            }

            double amount = _rand.Next(0, 10);
            double alicePays = sign * amount;
            double bobPays = alicePays * -1;

            TransactionPart alice = new TransactionPart("Alice", (decimal)alicePays);
            TransactionPart bob = new TransactionPart("Bob", (decimal)bobPays);
            Transaction transaction = new Transaction(alice, bob);

            return transaction;
        }

        public Example.Blockchain GetSampleChain()
        {
            const int howMany = 25;
            string parentHash = "";

            Example.Blockchain chain = new Example.Blockchain();
            BlockchainHelper helper = new BlockchainHelper();

            for (int i = 1; i <= howMany; i++)
            {
                Transaction tx = CreateSampleTransaction();
                Block innerBlock = helper.CreateBlock(tx, chain);
                chain.Chain.Add(innerBlock);
            }

            return chain;
        }
    }
}
