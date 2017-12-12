using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devRantDotNet.Tests
{
    [TestClass]
    public class Tests
    {
        devRant dr = new devRant();

        [TestMethod]
        public void GetRantsAsyncTest()
        {
            var result = dr.GetRantsAsync(devRant.SortType.algo).Result;
            Assert.IsTrue(result.Count == 30);
        }

        [TestMethod]
        public void GetRantAsyncTest()
        {
            int id = 867850;
            var result = dr.GetRantAsync(id).Result;
            Assert.AreEqual("px06", result.user_username);
        }
        
        [TestMethod]
        public void GetUserIdAsyncTest()
        {
            string username = "px06";
            var result = dr.GetUserIdAsync(username).Result;
            Assert.AreEqual(result, 428514);
        }

        [TestMethod]
        public void GetProfileAsyncTest()
        {
            long id = 428514;
            var result = dr.GetProfileAsync(id).Result;
            Assert.AreEqual("redrails", result.github);
            Assert.AreEqual("px06", result.username);
        }

        [TestMethod]
        public void SearchAsyncTest()
        {
            string term = "api";
            var result = dr.SearchAsync(term).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetRandomRantAsyncTest()
        {
            var result = dr.GetRandomRantAsync().Result;
            Assert.IsTrue(!string.IsNullOrEmpty(result.text));
        }

    }
}
