using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = "Hello Signatures";
            var signature = Crypto101.Signer.Sign(data, "cert.pfx", "Pass@Word1");

            Console.WriteLine(data);
            Console.WriteLine(signature);

            var signers = Crypto101.Signer.Verify(data, signature);
            Console.WriteLine(signers);

            var bad = Crypto101.Signer.Verify(data+data, signature);
            Console.WriteLine(bad);
        }
    }
}
