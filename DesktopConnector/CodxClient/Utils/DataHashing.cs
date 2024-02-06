using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft;
namespace CodxClient.Utils
{
    public class DataHashing
    {
        public static string HashBytes(byte[] data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hash = sha256Hash.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
        public static string HashText(string Text)
        {
            byte[] data = Encoding.UTF8.GetBytes(Text);
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hash = sha256Hash.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
        public static string HashDict(Dictionary<string, object> data)
        {
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);
            return HashBytes(jsonBytes);
        }

        public static bool IsValidHashKey(string HashkeyValue)
        {
            int expectedHashLength = 64;

            // Regular expression pattern for hexadecimal characters (modify for other bases)
            string pattern = @"^[0-9a-fA-F]+$";

            if (HashkeyValue.Length == expectedHashLength && System.Text.RegularExpressions.Regex.IsMatch(HashkeyValue, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
