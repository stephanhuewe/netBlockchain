using System.Collections.Generic;
using System.Linq;

namespace Huestel.Blockchain.Example
{
    public class TransactionManager
    {
        public Transaction DoTransaction()
        {
            int sign = -1;
            decimal amount = 100;
            decimal alicePays = sign * amount;
            decimal bobPays = alicePays * -1;

            TransactionPart alice = new TransactionPart("Alice", alicePays);
            TransactionPart bob = new TransactionPart("Bob", bobPays);
            Transaction transaction = new Transaction(alice, bob);

            return transaction;

        }



        /// <summary>
        /// Updates the status without any checks
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> UpdateStatus(Dictionary<string, decimal> transactions,
            Dictionary<string, decimal> states)
        {
            foreach (var key in transactions.Keys)
            {
                if (states.ContainsKey(key))
                {
                    states[key] += transactions[key];
                }
                else
                {
                    states[key] = transactions[key];
                }
            }

            return states;
        }

        /// <summary>
        /// Checks if the transaction is valid
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public bool IsValidTransaction(Dictionary<string, decimal> transactions, Dictionary<string, decimal> states)
        {
            // Sum of deposits and withdrawals must be 0
            decimal sum = transactions.Select(x => x.Value).Sum();
            if (sum != 0)
            {
                return false;
            }

            foreach (var key in transactions.Keys)
            {
                decimal balance = 0;

                // Existing balance
                if (states.Keys.Contains(key))
                {
                    balance = states[key];
                }

                decimal overpay = balance + transactions[key];
                if (overpay < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}