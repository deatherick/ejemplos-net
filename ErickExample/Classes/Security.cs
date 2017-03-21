using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ErickExample.Interfaces;

namespace ErickExample.Classes
{
    internal class Security : ISecurity
    {
        public string sha256_hash(string value)
        {
            using (var hash = SHA256.Create())
            {
                return string.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        public string md5_hash(string content)
        {
            var m5 = new MD5CryptoServiceProvider();
            var byteString = Encoding.ASCII.GetBytes(content);
            byteString = m5.ComputeHash(byteString);
            var finalString = byteString.Aggregate<byte, string>(null, (current, bt) => current + bt.ToString("x2"));
            return finalString.ToUpper();
        }

    }
}
