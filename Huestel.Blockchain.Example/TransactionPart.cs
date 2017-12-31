namespace Huestel.Blockchain.Example
{
    public class TransactionPart
    {
        public TransactionPart(string person, decimal amount)
        {
            Person = person;
            Amount = amount;
        }
        public string Person { get; }
        public decimal Amount { get;  }
    }
}