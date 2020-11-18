using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace RsaDemo
{
    [TestFixture]
    public class RsaTest
    {
        [Test]
        public void Test()
        {
            //how to generate public key and private key
            //https://stackoverflow.com/a/1308131/13338936
            RSA rsa = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key
            string publicPrivateKeyXml = rsa.ToXmlString(true);
            string publicOnlyKeyXml = rsa.ToXmlString(false);
            Console.WriteLine(publicOnlyKeyXml);
            Console.WriteLine(publicPrivateKeyXml);
        }

        [Test]
        public void Test2()
        {
            //test sample https://www.cnblogs.com/chucklu/p/14001408.html
            string publicKeyHexString =
                @"85da3d445b203823daa4727eb5701d9c405ed6b1b658d098fb5e8cbbbe2d10b6d885fc4bbee105d7e5c0d6c970fff5973c647ee8a201239f379796b628203ac72d9d05d18a5796a30ad4b8fbde73cc398057ad856c971567e5d5f8fe47785518417e0947ec9df8a63c517bbc3783cede111fc5467ee8df1a5b8e74abd5cf46da876ca61f74b5bb998dc7021eb397fc55585137f74763d249ce256fc92437a894230071a70d95d737928231120b665a0ca3e48124c9a001525070cf6f0bf793461d506ce67f7527c24aa3b70c8bd327eae19abebe27393e3927ac0d06789e129dae52e455084eec63a4bc35470ecf71a65c2ff78a3596841f3ed998d4acc6d1ed";

            string privateKeyHexString =
                "726503fb898dcdad06cd8874b607eda67e750f33ae4dd569095bd31718ff56cb8ddd64b42f9c0cec691517fbed3133e95ed9dc8461006c3b44bdaf365ab0c0cb3d3677a48f812fe283fd2d6344c8de7f3e2ab0c7d8f87e78df3ab1a44fdc8d8d3f5bc1fed0406a235865a3444685c5a4902a00e5b0ccc0efbbd3d1ee91baa6281c32c85dd43f04e10cba4202c5726c5566dd2f3204b2db219eaf311ef9659ca0eff04b9ce37cf2dff3fd067b3291d117c7bfb2e00e4aad17c97353b280326ab90dd97bd07a3f022b87c835dc299cb81d297c2ea3b6bd345833d1ab5915d242bd70224e4a3c0804d05e374e1ad81a10d87e78169189a5a948ee2c2a89e119bb59";

            //https://stackoverflow.com/a/46896803/13338936
            string exponant = "010001";
            string toEncrypt = "123";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                //RSA.
                //RSAParameters rsap = new RSAParameters
                //{
                //    Modulus = HexStringToByteArray(publicKeyHexString),
                //    //Exponent = HexStringToByteArray(exponant)
                //};
                ////Tried with and without the whole base64 thing
                //rsa.ImportParameters(rsap);
                // rsa.PersistKeyInCsp
                //byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), false);
                //string base64Encrypted = Convert.ToBase64String(encryptedData);
                //string str = ByteArrayToHexString(encryptedData);
                //Console.WriteLine(str);
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

        public static string ByteArrayToHexString(byte[] array)
        {
            string str = string.Join(string.Empty, array.Select(x => x.ToString("x2")));
            return str;
        }
    }
}
