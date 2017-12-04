using System;
using System.Collections.Generic;
using System.Linq;

namespace Huestel.Blockchain.Example
{
    public class Miner
    {
        public bool CheckBlockHash(Block block)
        {
            // This can be expensive. That's why miners are rewarded.
            string comparisonHash = Hash.CreateHash(block.Content.ToString());

            if (block.Hash != comparisonHash)
            {
                return false;
            }
            return true;
        }

        public Dictionary<string, decimal> CheckBlock(Block block, Block parent, Dictionary<string, decimal> currentState, out List<string> errors)
        {
            // Check if:
            // - Transactions are valid
            // - References within the chain are OK (Number + ParentHash)
            // - Block hash is valid
            errors = new List<string>();

            int parentBlockNumber = parent.Content.BlockNumber;
            string parentHash = parent.Hash;
            string parentHashStoredInCurrentBlock = block.Content.ParentHash;
            int currentBlockNumber = block.Content.BlockNumber;

            TransactionManager transactionManager = new TransactionManager();

            // Transaction
            // Todo: Foreach?
            bool transactionIsValid = transactionManager.IsValidTransaction(block.Content.Transactions, currentState);
            if (transactionIsValid)
            {
                currentState = transactionManager.UpdateStatus(block.Content.Transactions, currentState);
            }
            else
            { 
                errors.Add("Transaction is invalid!");
            }

            bool checkBlockHash = CheckBlockHash(block);
            if (!checkBlockHash)
            {
                errors.Add(String.Format("Hash of block {0} does not match content and is invalid", currentBlockNumber));
            }

            if (currentBlockNumber  != parentBlockNumber + 1)
            {
                errors.Add(String.Format("Hash of block {0} does not match content and is invalid", currentBlockNumber));
            }

            if (parentHash != parentHashStoredInCurrentBlock)
            {
                errors.Add(String.Format("Parent hash of block {0} does not match parent hash in chain", currentBlockNumber));
            }
            
            return currentState;
        }

        public Dictionary<string, decimal> CheckBlockchain(Blockchain chain)
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();

            TransactionManager manager = new TransactionManager();
            foreach (Block block in chain.Chain)
            {
                manager.UpdateStatus(block.Content.Transactions, states);
            }
            CheckBlockHash(chain.Chain[0]);
            var parent = chain.Chain[0];

            List<string> errors;
            foreach (Block block in chain.Chain.Skip(1))
            {
                states = CheckBlock(block, parent, states, out errors);
                parent = block;

                if (errors.Count != 0)
                {
                    throw new Exception(String.Format("Block {0} is invalid: {1}", block.Content.BlockNumber,
                        string.Join(",", errors)));
                }
            }

            return states;
        }
    }
}
