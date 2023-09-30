using System.Security.Cryptography;
using System.Text;

namespace MemberManagementAPI.Utilities
{
    public class Utilities
    {
        public static string Hash256Password(string password)
        {

            var sha = SHA256.Create();
            var bytedPassword = Encoding.UTF8.GetBytes(password);
            var hashedpassword = sha.ComputeHash(bytedPassword);

            return Convert.ToBase64String(hashedpassword);
        }
    }
}
