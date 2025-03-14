using System.Security.Cryptography;

namespace Itransition.Trainee
{
    public class FairRandomGenerator
    {
        private const int DATA_SIZE = 4;

        public static int GenerateSecureRandom(int maxValue)
        {
            using var randomValue = RandomNumberGenerator.Create();
            byte[] data = new byte[DATA_SIZE];
            randomValue.GetBytes(data);
            return (int)(BitConverter.ToUInt32(data, 0) % (maxValue + 1));
        }
    }
}
