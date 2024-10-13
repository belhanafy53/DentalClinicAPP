using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
 
    public class Security
    {
        //Private Memebers :

        private static readonly DESCryptoServiceProvider MobjCryptoService = new DESCryptoServiceProvider();


        //Public Memebers

        /// <summary>
        /// Prevent Some Symbols Chars From Query String
        /// </summary>
        /// <param name="controlInput">String To Filter it From Sql Injection</param>
        /// <returns>Return the Clear sql Data [Securied Data]</returns>
        public static string SqlInjection(string controlInput)
        {
            controlInput = controlInput.Replace("[", "[[]");
            controlInput = controlInput.Replace("%", "[%]");
            controlInput = controlInput.Replace("_", "[_]");
            controlInput = controlInput.Replace("'", "''");
            return controlInput;
        }
        /// <summary>
        /// Encrypt String By MD5 Encryptions [One Way Encryption]
        /// </summary>
        /// <param name="sourceText">The Text to Encrypt it</param>
        /// <returns>The Encrypted Data String</returns>
        public static string Encrypt_MD5(string sourceText)
        {
            //Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding ue = new UnicodeEncoding();
            //Retrieve a byte array based on the source text
            byte[] byteSourceText = ue.GetBytes(sourceText);
            //Instantiate an MD5 Provider object
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Compute the hash value from the source
            byte[] byteHash = md5.ComputeHash(byteSourceText);
            //And convert it to String format for return
            return Convert.ToBase64String(byteHash);
        }
        /// <summary>
        /// Set Key to Encrypt By DES (Two Way Encryption)
        /// </summary>
        /// <returns>Key of Encryption [Bytes]</returns>
        private static byte[] GetKey()
        {
            string key;
            key = "I'm";
            string sTemp;
            if (MobjCryptoService.LegalKeySizes.Length > 0)
            {
                int moreSize = MobjCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits
                while (key.Length * 8 > moreSize)
                {
                    moreSize += MobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = key.PadRight(moreSize / 8, ' ');
            }
            else
            {
                sTemp = key;
            }

            // convert the secret key to byte array
            return Encoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// Encrypt Text by DES (Two Way Encryption)
        /// </summary>
        /// <param name="source">Simple Text To Encrypt it</param>
        /// <returns>The Encrypted String</returns>
        public static string Encrypting(string source)
        {
            byte[] bytIn = Encoding.UTF8.GetBytes(source);
            // create a MemoryStream so that the process can be done without I/O files
            MemoryStream ms = new MemoryStream();

            byte[] bytKey = GetKey();

            // set the private key
            MobjCryptoService.Key = bytKey;
            MobjCryptoService.IV = bytKey;

            // create an Encryptor from the Provider Service instance
            ICryptoTransform encrypto = MobjCryptoService.CreateEncryptor();

            // create Crypto Stream that transforms a stream using the encryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            // write out encrypted content into MemoryStream
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            // get the output and trim the '\0' bytes
            byte[] bytOut = ms.GetBuffer();
            int i;
            for (i = 0; i <= bytOut.Length - 1; i++)
            {
                if (bytOut[i] == 0)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            // convert into Base64 so that the result can be used in xml
            return Convert.ToBase64String(bytOut, 0, i);
        }
        /// <summary>
        /// Decrypt Text by DES (Two Way Encryption)
        /// </summary>
        /// <param name="source">Encrypted Data String</param>
        /// <returns>The Simple Text</returns>
        public static string Decrypting(string source)
        {
            // convert from Base64 to binary
            byte[] bytIn = Convert.FromBase64String(source);
            // create a MemoryStream with the input
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);

            byte[] bytKey = GetKey();

            // set the private key
            MobjCryptoService.Key = bytKey;
            MobjCryptoService.IV = bytKey;

            // create a Decryptor from the Provider Service instance
            ICryptoTransform encrypto = MobjCryptoService.CreateDecryptor();

            // create Crypto Stream that transforms a stream using the decryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            // read out the result from the Crypto Stream
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
