using System.Linq;
using System.Security.Cryptography;

namespace Infostracture
{
    public class SecurityAlphaNumericGenerator
    {
        private static readonly char[] sequency;
        static SecurityAlphaNumericGenerator()
        {
            sequency = Enumerable.Range(0, 10)
                .Concat(Enumerable.Range('A', 'Z' - 'A' + 1))
                .Concat(Enumerable.Range('a', 'z' - 'a' + 1))
                .Select(code => (char)code)
                .ToArray();
        }

        public char[] GetChars(int count)
        {
            var length = sequency.Length;

            return RandomNumberGenerator.GetBytes(count)
                .Select(number => sequency[number % length]).ToArray();
        }
    }
}