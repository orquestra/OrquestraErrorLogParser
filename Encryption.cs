using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace OrquestraErrorLogParser
{
    public static class Encryption
    {
        public static string AES_DEFAULT_KEY = "askdjlk@#$329dsadkas(*#AS423S2@w";
        public static string DES_DEFAULT_KEY = "!#$a54?3";

        public static string Dec(string EncryptionAlgorithm, string QueryString, string Key)
        {
            if (EncryptionAlgorithm == "AES")
            {
                QueryString = HttpUtility.UrlDecode(QueryString);
                return RijndaelDecrypt(QueryString, Key);
            }
            else
            {
                return DESDecrypt(QueryString, Key);
            }
        }

        private static string RijndaelDecrypt(string QueryString, string Key)
        {

            byte[] dados = System.Convert.FromBase64String(QueryString.Replace(" ", "+"));

            byte[] pw = Encoding.UTF8.GetBytes(Key);

            RijndaelManaged rm = new RijndaelManaged();
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Key, new MD5CryptoServiceProvider().ComputeHash(pw));
            rm.Key = pdb.GetBytes(32);
            rm.IV = pdb.GetBytes(16);

            rm.BlockSize = 128;
            rm.Padding = PaddingMode.PKCS7;

            MemoryStream ms = new MemoryStream(dados, 0, dados.Length);

            CryptoStream cryptStream = new CryptoStream(ms, rm.CreateDecryptor(rm.Key, rm.IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cryptStream);

            return sr.ReadToEnd();

        }

        private static string DESDecrypt(string QueryString, string Key)
        {
            Cryo.Work.Encryption64 oES = new Cryo.Work.Encryption64();
            return oES.Decrypt(QueryString, Key);
        }
    }
}
