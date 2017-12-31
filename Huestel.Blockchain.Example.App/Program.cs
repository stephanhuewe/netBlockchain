using System;
using System.Collections.Generic;
using System.Globalization;

namespace Huestel.Blockchain.Example.App
{
    class Program
    {
        private const string ALICE = "Alice";
        private const string BOB = "Bob";

        static void Main(string[] args)
        {
            Random rand = new Random();

            while (true)
            {
                PlayBlockchain(rand);
            }
        }

        private static void PlayBlockchain(Random rand)
        {
            Console.WriteLine(String.Format("{0}: Start", DateTime.Now));
            Console.WriteLine(String.Format("{0}: Creating Blockchain", DateTime.Now));

            Example.Blockchain chain = new Example.Blockchain();
            BlockchainHelper helper = new BlockchainHelper();

            for (int i = 1; i <= 1000; i++)
            {
                int sign = 1;
                if (rand.Next(0, 2) == 0)
                {
                    sign = -1;
                }

                byte[] buff = new byte[2];
                rand.NextBytes(buff);

                // ToDo: Find a better way for random number
                string number = "0" + Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) + buff[0] + buff[1];
                double amount = Convert.ToDouble(number);

                double personA = sign * amount;
                double personB = personA * -1;

                TransactionPart alice = new TransactionPart(ALICE, (decimal) personA);
                TransactionPart bob = new TransactionPart(BOB, (decimal) personB);
                Transaction transaction = new Transaction(alice, bob);

                Console.WriteLine(String.Format("{0}: Creating transaction {1} - {2}", DateTime.Now, i,
                    transaction.ToString()));

                Block innerBlock = helper.CreateBlock(transaction, chain);
                chain.Chain.Add(innerBlock);
            }

            Console.WriteLine(String.Format("{0}: Start mining", DateTime.Now));
            Miner miner = new Miner();
            Dictionary<string, decimal> result = miner.CheckBlockchain(chain);

            Console.WriteLine(String.Format("{0}: Final result is {1}:{2} and {3}:{4}", DateTime.Now, ALICE, result[ALICE], BOB,
                result[BOB]));

            Console.WriteLine("Press any key to start over");
            Console.ReadKey();
        }
    }
}
