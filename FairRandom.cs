using System.Security.Cryptography;

namespace Itransition.Trainee
{
    public class FairRandom
    {
        private byte[] _secretKey;
        private int _maxValue;
        private int _generatedValue;
        private HmacCalculator _hmacCalculator;

        private const int SECRET_KEY_SIZE = 32;

        public FairRandom(int maxValue)
        {
            GenerateNewKey();
            _maxValue = maxValue;
            _generatedValue = FairRandomGenerator.GenerateSecureRandom(_maxValue);
            _hmacCalculator = new HmacCalculator(_secretKey);
        }

        private void GenerateNewKey()
        {
            _secretKey = RandomNumberGenerator.GetBytes(SECRET_KEY_SIZE);
        }

        public string GetHMAC()
        {
            return _hmacCalculator.CalculateHMAC(_generatedValue);
        }

        public int RevealSecret(out string key)
        {
            key = Convert.ToHexString(_secretKey);
            GenerateNewKey();
            _generatedValue = FairRandomGenerator.GenerateSecureRandom(_maxValue);
            return _generatedValue;
        }
    }
}