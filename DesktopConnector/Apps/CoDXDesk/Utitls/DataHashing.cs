using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft;
namespace CoDXDesk.Utils
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
    }
}
