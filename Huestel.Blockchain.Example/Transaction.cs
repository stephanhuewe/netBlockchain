using System;
using Newtonsoft.Json;

namespace Huestel.Blockchain.Example
{
    public class Transaction
    {
        public Transaction(TransactionPart transactionPart, TransactionPart transactionCounterPart)
        {
            Currentransaction = new Tuple<TransactionPart, TransactionPart>(transactionPart, transactionCounterPart);
        }
        public Tuple<TransactionPart,TransactionPart> Currentransaction
        {
            get;
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
