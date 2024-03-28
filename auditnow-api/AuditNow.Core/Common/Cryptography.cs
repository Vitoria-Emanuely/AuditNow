#region Using
using System.Security.Cryptography;
#endregion

namespace AuditNow.Core.Common
{
    public class Cryptography
    {


        public string Encrypt(string pText)
        {
            if (pText.Length == 0)
            {
                return pText;
            }

            MemoryStream msEncrypt = null;
            StreamWriter swEncrypt = null;
            CryptoStream csEncrypt = null;

            using (Aes aesAlg = Aes.Create())
            {
                try
                {
                    byte[] key = System.Text.ASCIIEncoding.ASCII.GetBytes("9371049284016583");
                    byte[] iV = System.Text.ASCIIEncoding.ASCII.GetBytes("9371049284016583");
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iV);
                    msEncrypt = new MemoryStream();
                    csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                    swEncrypt = new StreamWriter(csEncrypt);
                    swEncrypt.Write(pText);
                }
                finally
                {
                    if (swEncrypt != null) swEncrypt.Close();
                    if (csEncrypt != null) csEncrypt.Close();
                    if (msEncrypt != null) msEncrypt.Close();
                    if (aesAlg != null) aesAlg.Clear();
                }
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }


        public string Decrypt(string pText)
        {
            if (pText.Length == 0)
            {
                return pText;
            }

            byte[] cipherText = Convert.FromBase64String(pText);
            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                try
                {
                    byte[] key = System.Text.ASCIIEncoding.ASCII.GetBytes("9371049284016583");
                    byte[] iV = System.Text.ASCIIEncoding.ASCII.GetBytes("9371049284016583");
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iV);
                    msDecrypt = new MemoryStream(cipherText);
                    csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    srDecrypt = new StreamReader(csDecrypt);
                    plaintext = srDecrypt.ReadToEnd();
                }
                finally
                {
                    if (srDecrypt != null) srDecrypt.Close();
                    if (csDecrypt != null) csDecrypt.Close();
                    if (msDecrypt != null) msDecrypt.Close();
                    if (aesAlg != null) aesAlg.Clear();
                }

            }

            return plaintext;
        }


    }
}