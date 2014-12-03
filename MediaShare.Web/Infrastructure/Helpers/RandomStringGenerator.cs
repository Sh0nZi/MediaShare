namespace MediaShare.Web.Infrastructure.Helpers
{
    using System;
    using System.Text;

    public class RandomStringGenerator
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private static readonly Random rand = new Random();

        public static string GenerateString()
        {
            var sb = new StringBuilder();
            var len = rand.Next(4, 10);
            for (int i = 0; i < len; i++)
            {
                sb.Append(Letters[rand.Next(0, Letters.Length)]);
            }
            return sb.ToString();
        }
    }
}