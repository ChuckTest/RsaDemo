using System;
using System.Security.Cryptography;
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
    }
}
