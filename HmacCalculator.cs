using System.Security.Cryptography;
using System.Text;

namespace Itransition.Trainee
{
    public class HmacCalculator
    {
        private byte[] _secretKey;

        public HmacCalculator(byte[] secretKey)
        {
            _secretKey = secretKey;
        }

        public string CalculateHMAC(int value)
        {
            using var hmac = new HMACSHA256(_secretKey);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value.ToString()));
            return Convert.ToHexString(hash);
        }
    }
}
