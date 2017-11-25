using System;
using System.Security.Cryptography;
using System.Text;

namespace Huestel.Blockchain.Example
{
    public class Manager
    {
        public string CreateHash(string payload)
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
    }
}
