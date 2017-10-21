using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;

namespace Crypto101
{
    public class Signer
    {
        public static string Sign(string data, string certPath, string password)
        {
            var signedCms = new SignedCms(new ContentInfo(Encoding.UTF8.GetBytes(data)), true);
            signedCms.ComputeSignature(new CmsSigner(new X509Certificate2(certPath, password)));                        
            return Convert.ToBase64String(signedCms.Encode());
        }
        public static string Verify(string data, string signature)
        {
            var signedCms = new SignedCms(new ContentInfo(Encoding.UTF8.GetBytes(data)), true);
            signedCms.Decode(Convert.FromBase64String(signature));
            try
            {
                signedCms.CheckSignature(true);
                return $"\nSignerInfo: \n Subject: {signedCms.SignerInfos[0].Certificate.Subject}" +
                        $"\n SerialNumber: {signedCms.SignerInfos[0].Certificate.SerialNumber}" +
                        $"\n ThumbPrint: {signedCms.SignerInfos[0].Certificate.Thumbprint}";

            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("EXCEPTION");
                Console.WriteLine(ex.ToString());
                
                return "BAD SIGNATURE";
            }
        }
    }
}
