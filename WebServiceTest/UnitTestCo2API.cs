using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Net.Http;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest
{
    [TestClass]
    public class UnitTestCo2Api
    {
        [Test]
        public void TestConnection()
        {
            using (var client = new HttpClient())
            {
                String URL = "https://localhost:5001/CO2";
                var response = client.GetStringAsync(URL);
                string result = response.ToString();
                Assert.IsTrue(result != null);
            }
        }

        //TODO will need changing after adding user to get specific device/room co2 
        [Test]
        public void TestGetCo2()
        {
            using (var client = new HttpClient())
            {
                String URL = "https://localhost:5001/CO2";
                var response = client.GetStringAsync(URL);
                Assert.IsTrue(Int16.Parse(response.Result) < 5000 && Int16.Parse(response.Result) > 410);
            }
        }
    }
}
