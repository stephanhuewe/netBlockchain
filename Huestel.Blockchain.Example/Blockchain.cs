using System;
using System.Collections.Generic;

namespace Huestel.Blockchain.Example
{
    public class Blockchain
    {
        private readonly List<Block> _chain;

        public Blockchain()
        {
            // This is the very first aka genesis transaction
            Dictionary<string, decimal> root = new Dictionary<string, decimal>();
            root.Add("Alice", 100);
            root.Add("Bob", 100);

            string parentHash = String.Empty;
            int blockNumber = 0;
            int NumberOfTransactions = 1;

            BlockContent content = new BlockContent(blockNumber, parentHash, NumberOfTransactions, root);

            _chain = new List<Block>();
            Block rootBlock = new Block(content);
            Chain.Add(rootBlock);

        }

        public List<Block> Chain
        {
            get { return _chain; }
        }

        public int Length
        {
            get { return _chain.Count; }
        }

    }
}
