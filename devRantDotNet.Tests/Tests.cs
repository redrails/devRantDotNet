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
            int id = 450227;
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
            Assert.IsTrue(result.rants.Find(x => x.id == 450227).tags.Contains("c#"));
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
            Assert.IsTrue(result.num_upvotes >= 20);
        }

    }
}
