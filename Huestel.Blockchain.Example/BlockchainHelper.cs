using System.Collections.Generic;

namespace Huestel.Blockchain.Example
{
    public class BlockchainHelper
    {
        public Block CreateBlock(Transaction transaction, Blockchain blockchain)
        {
            Dictionary<string, decimal> transactions = ConvertTransaction(transaction);
            Block lastBlock = blockchain.Chain[blockchain.Length - 1];
            string parentHash = lastBlock.Hash;
            int blockNumber = lastBlock.Content.BlockNumber + 1;
            int numberOfTransacitons = transactions.Count;

            BlockContent content = new BlockContent(blockNumber, parentHash, numberOfTransacitons, transactions);
            Block block = new Block(content);

            return block;
        }

        private Dictionary<string, decimal> ConvertTransaction(Transaction transaction)
        {
            Dictionary<string, decimal> ret = new Dictionary<string, decimal>();

            ret.Add(transaction.Currentransaction.Item1.Person, transaction.Currentransaction.Item1.Amount);
            ret.Add(transaction.Currentransaction.Item2.Person, transaction.Currentransaction.Item2.Amount);

            return ret;
        }
    }
}