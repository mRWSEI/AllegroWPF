using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllegroClass;

namespace AllegroUnitTest
{
    [TestClass]
    public class AllegroUnitTest
    {
        [TestMethod]
        public void ValidAuth()
        {
            string clientId = "0069626c63e2456282e868e6ee62cad2";
            string clientSecret = "oUjfWSVkSC4FDNGFHnqBkqJV3mBLPyRpAzadKOpICPF6kq2tFJQSlsaImffod9w4";
            //string clientId = "fea6365501924bf88bfd2edce1e63f90";
            //string clientSecret = "f21L9u8mZPDuFD5G1hAVbbeJckW7NqbYGvq3fUuIAGVBomULYNFxNIMcdlVUqOZQ";
            AllegroRest rest = new AllegroRest(clientId, clientSecret);
            var result = rest.GetTokenJ().Result;
            System.Diagnostics.Debug.Write($"{rest.accessToken}");
            Assert.AreNotEqual("", rest.accessToken);
        }

        [TestMethod]
        public void InValidAuth()
        {
            string clientId = "006";
            string clientSecret = "oUjfWSVk";
            AllegroRest rest = new AllegroRest(clientId, clientSecret);
            var result = rest.GetTokenJ().Result;
            System.Diagnostics.Debug.Write($"{rest.accessToken}");
            Assert.AreEqual("", rest.accessToken);
        }
    }
}
