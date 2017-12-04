using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huestel.Blockchain.Example
{
    public class BlockContent
    {
        public BlockContent(int blockNumber, string parentHash, int numberOfTransactions, Dictionary<string, decimal> transactions)
        {
            BlockNumber = blockNumber;
            ParentHash = parentHash;
            NumberOfTransactions = numberOfTransactions;
            Transactions = transactions;

        }
        public int BlockNumber { get; private set; }
        public string ParentHash { get; private set; }
        public int NumberOfTransactions { get; private set; }
        public Dictionary<string, decimal> Transactions { get; private set; }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
