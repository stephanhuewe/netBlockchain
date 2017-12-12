# netBlockchain
Blockchain technology implementation in C# / .net

![alt text](https://www.netblockchain.de/wp-content/uploads/2017/12/cropped-white_logo_color_background.jpg)

## Why NETblockchain?
Blockchain is widely used in cryptocurrencies. But apart from that this technology can be used for various other representations of coherent data.

## The Basics
A blockchain is a kind of distributed database. Every participant can save a copy of the database so there is no central service. The database can be extended by adding new data. Every new dataset contains a validation (e.g. hash) of the parent data. The algorithm for proofing transactions within the blockchain is public - so everyone can validate the blockchain. Once data is written to the blockchain it cannot be modified anymore. Blockchains can be considered to be very secure. Any manipulation would mean that you have to rewrite and recalc all blocks from the beginning. In general this is rather economically unviable.
Miners are considered to validate the blockchain. This costs some CPU power (and by this energy) - that is why they are rewarded with some transaction fee taken from each new transaction.

## Hashing
There is some hash algorithm which is used to link the blocks with each other. You can find it within hash.cs:

```
public static string CreateHash(string payload)
        {
            // Sorting?
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
