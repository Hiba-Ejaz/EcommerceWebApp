using System.Text;
using System.Security.Cryptography;

namespace WebApi.Business.src.Implementations.Shared
{
    public class PasswordService
    {
        public static void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
        {
            var hmac = new HMACSHA256();
            salt = hmac.Key;
            hashedPassword = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        }
        public static bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
        {
            var hmac = new HMACSHA256(salt); //will generate the same salt as parameter is given inside
            var originalhashedPassword = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
            return originalhashedPassword == hashedPassword;
        }
    }
}