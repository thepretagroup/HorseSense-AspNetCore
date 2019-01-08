using HorseSense_AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace HorseSenseTest
{
    public class HorseSenseTestContextTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreationTest()
        {
            var context = new HorseSenseTestContext();

            Assert.IsNotNull(context);
            Assert.AreEqual("Microsoft.EntityFrameworkCore.InMemory", context.Database.ProviderName);
        }

        [Test]
        public void TwoContextInSameMethodTest()
        {
            var context1 = new HorseSenseTestContext();
            Assert.IsNotNull(context1);

            context1.Horses.Add(new Horse { Name = "TwoContextInSameMethodTest" });
            context1.SaveChanges();
            Assert.AreEqual(1, context1.Horses.Count());
            Assert.AreEqual("TwoContextInSameMethodTest", context1.Horses.Single<Horse>().Name);

            var context2 = new HorseSenseTestContext();
            Assert.AreEqual(1, context2.Horses.Count());
            Assert.AreEqual("TwoContextInSameMethodTest", context2.Horses.Single<Horse>().Name);
        }

        [Test]
        public void TwoDifferentContextFromDifferentMethodTest()
        {
            var context1 = GetContext1();
            Assert.IsNotNull(context1);

            context1.Horses.Add(new Horse { Name = "TwoContextInSameMethodTest" });
            context1.SaveChanges();
            Assert.AreEqual(1, context1.Horses.Count());

            var context2 = GetContext2();
            Assert.AreEqual(0, context2.Horses.Count());
        }

        private HorseSenseTestContext GetContext1()
        {
            return new HorseSenseTestContext();
        }
        private HorseSenseTestContext GetContext2()
        {
            return new HorseSenseTestContext();
        }
    }
}