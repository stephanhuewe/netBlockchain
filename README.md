# netBlockchain
Blockchain technology implementation in C# / .net

![alt text](https://www.netblockchain.de/wp-content/uploads/2017/12/cropped-white_logo_color_background.jpg)

## Why NETblockchain?
Blockchain is widely used in cryptocurrencies. But apart from that this technology can be used for various other representations of coherent data.

## The Basics
A blockchain is a kind of distributed database. Every participant can save a copy of the database so there is no central service. The database can be extended by adding new data. Every new dataset contains a validation (e.g. hash) of the parent data. The algorithm for proofing transactions within the blockchain is public - so everyone can validate the blockchain. Once data is written to the blockchain it cannot be modified anymore. Blockchains can be considered to be very secure. Any manipulation would mean that you have to rewrite and recalc all blocks from the beginning. In general this is rather economically unviable.
Miners are considered to validate the blockchain. This costs some CPU power (and by this energy) - that is why they are rewarded with some transaction fee taken from each new transaction.

## Hashing
There is some hash algorithm which is used to link the blocks with each other. You can find it within Hash.cs:

```
public static string CreateHash(string payload)
        {
            byte[] message = Encoding.UTF8.GetBytes(payload);
            SHA256Managed hashString = new SHA256Managed();
            string hex = String.Empty;

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
```
I decided for SHA256 - but there can be alternatives.

## Transaction Basics
A basic transaction looks like this:

```
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
```
This is a sample transaction between Alice and Bob. Withdrawals are shown as negative numbers and all deposits as positive numbers. A transaction consists of two parts. Since money cannot vanish the sum of both transaction parts has to be zero in any condition.

## Block Basics
Now we should create some blocks. But before starting with it we should think about the basic rules of our blocks.

### Basic Rules
The first rule was already defined earlier: The sum of each transaction should be 0.
The second rule is that any transaction can only be performed if the balance of this user covers the transaction.

An implementation could look like this:

```
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
```
Of course I created some basic UnitTests in order to demonstrate the logic. 
This test has to fail since Alice' balance does not cover the tranaction (5 < 6).

```
[TestMethod]
        public void TransactionOverpayFailTest()
        {
            Dictionary<string, decimal> states = new Dictionary<string, decimal>();
            states.Add("Alice", 5);
            states.Add("Bob", 5);

            Dictionary<string, decimal> transactions = new Dictionary<string, decimal>();
            transactions.Add("Alice", -6);
            transactions.Add("Bob", 6);

            TransactionManager manager = new TransactionManager();
            bool result = manager.IsValidTransaction(transactions, states);

            Assert.IsFalse(result);
        }
```
This test has to to fail
