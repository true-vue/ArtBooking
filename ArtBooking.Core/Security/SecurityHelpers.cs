using System.Security.Cryptography;
using System.Text;

public static class SecurityHelpers
{
    public static string CalculateHMAC(string text, string key)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = hmac.ComputeHash(textBytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}