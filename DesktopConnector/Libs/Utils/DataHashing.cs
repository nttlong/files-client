using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
namespace Utils
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
            string jsonText = JsonSerializer.Serialize(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);
            return HashBytes(jsonBytes);
        }
    }
}
