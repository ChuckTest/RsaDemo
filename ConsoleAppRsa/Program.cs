using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleAppRsa
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Test1();
                //Test2();
                Test3();
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

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            if (ba == null)
            {
                return string.Empty;
            }
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static void Test1()
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
            Console.WriteLine(array.Length);
            Span<byte> bytes = array;
            var r1 = RSA.Create();

            r1.ImportRSAPublicKey(bytes, out int bytesRead);

            var temp = new RsaSecurityKey(r1);
            Console.WriteLine(temp.Parameters);
        }

        public static void Test2()
        {
            using RSA rsa = RSA.Create(2048);
            byte[] pkcs1PublicKey = rsa.ExportRSAPublicKey();
            Console.WriteLine($"pkcs1PublicKey.Length = {pkcs1PublicKey.Length}");
            var pkcs1PublicKeyHexString = ByteArrayToString(pkcs1PublicKey);
            Console.WriteLine(pkcs1PublicKeyHexString);

            byte[] pkcs1PrivateKey = rsa.ExportRSAPrivateKey();
            Console.WriteLine($"pkcs1PrivateKey.Length = {pkcs1PrivateKey.Length}");
            var pkcs1PrivateKeyHexString = ByteArrayToString(pkcs1PrivateKey);
            Console.WriteLine(pkcs1PrivateKeyHexString);
        }

        public static void Test3()
        {
            var rsa = RSA.Create(2048);

            string publicKeyHexString =
                @"3082010a0282010100d095bcfebef1361f3daa081254d017ddd3f7a3cb0fc71a9cf3bac8ccc1f72121ee26993388c1b44663bd083f1e8944b06a559e65ff777ef8d84c1fd4388318cfc74856afd63f116f999bc461e3e06d921f315ef40225f7077524faeaf1a7d10887e89f0b76f2ad95a87089d274d9b400a80e72bb4dfcad42380f02e746807ecebefb370392b78c7b125ea26fc27b50f9c470cf5d7b39f684b6d7318accbd9b5e36747f9ee54f8ab226444ba5728e34fef4ea06b3ed9cb6c65f8495314ca9442ea089db655de70e6a665fc5057be76d26b82cb2a16c8dc72ccf39cfacd926d732242496f480606d6626f3a4300a8ec583b43b4127a81544e2be19dde0c2d19f6d0203010001";
            var publicKeyByteArray = StringToByteArray(publicKeyHexString);
            Console.WriteLine(publicKeyByteArray.Length);
            Span<byte> publicKeySpanBytes = publicKeyByteArray;
            rsa.ImportRSAPublicKey(publicKeySpanBytes, out int bytesRead);
            var publicKeyRsaParameters = rsa.ExportParameters(false);
            PrintRsa(publicKeyRsaParameters);

            string privateKeyHexString = "308204a40201000282010100d095bcfebef1361f3daa081254d017ddd3f7a3cb0fc71a9cf3bac8ccc1f72121ee26993388c1b44663bd083f1e8944b06a559e65ff777ef8d84c1fd4388318cfc74856afd63f116f999bc461e3e06d921f315ef40225f7077524faeaf1a7d10887e89f0b76f2ad95a87089d274d9b400a80e72bb4dfcad42380f02e746807ecebefb370392b78c7b125ea26fc27b50f9c470cf5d7b39f684b6d7318accbd9b5e36747f9ee54f8ab226444ba5728e34fef4ea06b3ed9cb6c65f8495314ca9442ea089db655de70e6a665fc5057be76d26b82cb2a16c8dc72ccf39cfacd926d732242496f480606d6626f3a4300a8ec583b43b4127a81544e2be19dde0c2d19f6d0203010001028201010089c36c4940a9a5da6a6ae3dbdfa27530f0efed818f912c559f70ad70f76716be0741fd0b99767e6fc32e35c52290fd0a1ba122f6310da6920aa1f49fc1176d0ac68f5399dd42586cc222ac490f2dca90a9037db861b6db7a5477b135fd979e2b29408dd30fa3e6dc229cf99a43cd09e3291c29d0e6084e129f5de2c807bb94841d1dbf4ef8b6fb7b9cd175a067f3ce5192e50d9974b86c0ba4f8b1974452f1e357d1403cb17f8688edbb726223a3b330337e525976361bdf34116b5632eb491d4c938ba821d00a3029a7272f10630a3fb336bf9fa92dd26c5731bc9281c0ac8677883a0fcde28d510ccebd3c423514585dc1742858b9d2a2e653ffb54088c90d02818100eeac9b9c7f81224c4b83c7c234de144fab5743340c5b7f3685228552ba159ba98456669b7104801c1cf74ff36944a0574e552f7a0ce2a196461fca9f9c8423ac4129bd9c8fb51b82071a84d5d4b6e2f9f6d3b61513b35ac04b9ad99eb8b297d18291c45ae1140aedf8cda3298686842310e91fb5f332953eeb3ba5deb056a4bf02818100dfb9f7744c9c6ad7735537dd41211e3bb1db33ba77a0f87c25e9210fdcb470f67867054419590a4fb3b7fee2bd853a30c4fbd23ddad6bb28a2be54cb50bd1fbfb0152fd4af3a14bc7a2c38f09a77a4bb44e70361fa70c8ec228b18b014ed132952042da67f7bba32ce0f6c497986c6c891b589dcb3e28dc843087b69693faad30281807af39e623b68e9a3460f87fb6ac2b0d1ede1513461dd4ca66865452b3dc35de230d3559e63d53eadbcda466c8d6115fc83d4ec5542880957e19e0502be5d70070fb692003223c91cedcde27a494f4194256c20d3b0c7674bc266ee0e8ef529b27b41de5636b6dd7e83fa63404415cdc246b6e16818f588ded5cd6f804d7504ff0281800b1f3998047efa28bfc90abfeb0ef46bcf49b066a2afd3ea880ff0ded2fffd65a352abc658e43e48fc0c37959a0a577c392eb3a2f3901d39c5ed6455bf2bc7ba45cca7d610e7f843ee66e865bda818fe5e4c85b48738b824550b492ce7bd6968727556b7a23aca64e02d901a8efa0e43c64a9a11e8b76888fe1c1bd67faef49502818100a37367039cdcd5dd8446219fc911142bb33c8c9249098a6ac9f51885f1723042a2c83447b6e98da095c09c765edf39b3d0d2d778ee3f29b3a66d7572145a9e73e026b31f7ac33335da990c1f2dd0a4e1393026bf9f35a3fe75c68a70127b5db0074e026b21886ff96aea2835d07051facadfb11eb2e86ccad9115dc2a9988953";
            var privateKeyArray = StringToByteArray(privateKeyHexString);
            Console.WriteLine(privateKeyArray.Length);
            Span<byte> privateKeySpanBytes = privateKeyArray;
            rsa.ImportRSAPrivateKey(privateKeySpanBytes, out int bytesRead2);
            var privateKeyRsaParameters = rsa.ExportParameters(true);
            PrintRsa(privateKeyRsaParameters);

        }

        /// <summary>
        /// https://tools.ietf.org/html/rfc8017#appendix-A.1.1
        /// </summary>
        /// <param name="parameters"></param>
        public static void PrintRsa(RSAParameters parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //privateExponent is the RSA private exponent d.
            //privateExponent   INTEGER,  -- d
            stringBuilder.AppendLine($"parameters.D.Length = {parameters.D?.Length}");
            stringBuilder.AppendLine($"parameters.D = {ByteArrayToString(parameters.D)}");

            //exponent1 is d mod (p - 1).
            //exponent1         INTEGER,  -- d mod (p-1)
            stringBuilder.AppendLine($"parameters.DP.Length = {parameters.DP?.Length}");
            stringBuilder.AppendLine($"parameters.DP = {ByteArrayToString(parameters.DP)}");

            //exponent2 is d mod (q - 1).
            //exponent2         INTEGER,  -- d mod (q-1)
            stringBuilder.AppendLine($"parameters.DQ.Length = {parameters.DQ?.Length}");
            stringBuilder.AppendLine($"parameters.DQ = {ByteArrayToString(parameters.DQ)}");

            //publicExponent is the RSA public exponent e.
            //publicExponent    INTEGER,  -- e
            stringBuilder.AppendLine($"parameters.Exponent.Length = {parameters.Exponent.Length}");
            stringBuilder.AppendLine($"parameters.Exponent = {ByteArrayToString(parameters.Exponent)}");

            //coefficient is the CRT coefficient q^(-1) mod p.
            //coefficient       INTEGER,  -- (inverse of q) mod p
            stringBuilder.AppendLine($"parameters.InverseQ.Length = {parameters.InverseQ?.Length}");
            stringBuilder.AppendLine($"parameters.InverseQ = {ByteArrayToString(parameters.InverseQ)}");

            //modulus is the RSA modulus n.
            //modulus           INTEGER,  -- n
            stringBuilder.AppendLine($"parameters.Modulus.Length = {parameters.Modulus.Length}");
            stringBuilder.AppendLine($"parameters.Modulus = {ByteArrayToString(parameters.Modulus)}");

            //prime1 is the prime factor p of n.
            //prime1            INTEGER,  -- p
            stringBuilder.AppendLine($"parameters.P.Length = {parameters.P?.Length}");
            stringBuilder.AppendLine($"parameters.P = {ByteArrayToString(parameters.P)}");

            //prime2 is the prime factor q of n.
            //prime2            INTEGER,  -- q
            stringBuilder.AppendLine($"parameters.Q.Length = {parameters.Q?.Length}");
            stringBuilder.AppendLine($"parameters.Q = {ByteArrayToString(parameters.Q)}");

            Console.WriteLine(stringBuilder);
        }
    }
}
