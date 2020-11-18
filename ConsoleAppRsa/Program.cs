using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleAppRsa
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                string str =
                    @"5acdd0bc4fb26fb7696a04faa22e02651ad2bd8688d1c5228829265f81ae509d
23b77685e7f767fbc2b1a0ce90aca19280e832a55eaea5b515c8f2389cd9a098
8848ada2889760706c7bc3150ffb1433786bee907c7763ca15a688a95d7ae29d
0f1da0557351d653925b0fc97beb8b45703276a2d574855d7f6aebf64d70f568
1a79f2d890b4530deb754c2b1e29eeb54b0cab6e770bed434d9c95da0a4b5ea0
a4ef4aecc2c1cc6cbc2be413c4239e6684aaf10b95e4258bbcfa4b6049d0ac3a
0abf7bff85d76fcee8099f9fd977f2b6adc2eca0572cb85d18cd8a3040426915
f44969576b3a817876594336ca0563da8a036398b57cbac58943959e9d694c01";
                var publicKeyHexString = str.Replace(Environment.NewLine, string.Empty);
                Console.WriteLine(publicKeyHexString);
                var array = StringToByteArray(publicKeyHexString);
                Span<byte> bytes = array;
                var r1 = RSA.Create();

                r1.ImportRSAPublicKey(bytes, out int bytesRead);
                var temp = new RsaSecurityKey(r1);
                Console.WriteLine(temp.Parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }
        public static byte[] HexStringToByteArray(string hexString)
        {
            MemoryStream stream = new MemoryStream(hexString.Length / 2);

            for (int i = 0; i < hexString.Length; i += 2)
            {
                stream.WriteByte(byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            return stream.ToArray();
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
