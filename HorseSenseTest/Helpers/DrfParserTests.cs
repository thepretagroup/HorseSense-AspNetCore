using HorseSense_AspNetCore.Helpers;
using HorseSense_AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace HorseSenseTest.Helpers
{
    public class DrfParserTests
    {
        // Each test method specifies a unique database name, meaning each method has its own InMemory database.
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
        private DbContextOptions<HorseSenseContext> GetHorseSenseContextOptionForMethod(
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            Console.WriteLine("Caller = " + callerName);

            var options = new DbContextOptionsBuilder<HorseSenseContext>()
                .UseInMemoryDatabase(databaseName: callerName)
                .Options;

            // Run the test against one instance of the context
            return options;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DrfParserBaseTest()
        {
            // Run the test against one instance of the context
            using (var context = new HorseSenseContext(GetHorseSenseContextOptionForMethod()))
            {
                context.Horses.Add(new Horse { Name = "Horsie" });
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HorseSenseContext(GetHorseSenseContextOptionForMethod()))
            {
                Assert.AreEqual(1, context.Horses.Count());
                Assert.AreEqual("Horsie", context.Horses.Single().Name);
            }
        }

        [Test]
        public void InsertRaceDayTest()
        {
            ////////using (var context = new HorseSenseContext(GetHorseSenseContextOptionForMethod()))
            using (var context = new HorseSenseTestContext())
            {
                var raceday = new RaceDay()
                {
                    Track = "TestTrack",
                    Date = "20180102"
                };

                DrfParser.ParseLine(context, raceday);

                Assert.AreEqual(1, context.RaceDays.Count());
                Assert.AreEqual("TestTrack", context.RaceDays.Single().Track);
                Assert.AreEqual("20180102", context.RaceDays.Single().Date);
            }
        }

        [Test]
        public void DuplicateInsertRaceDayTest()
        {
            using (var context = new HorseSenseContext(GetHorseSenseContextOptionForMethod()))
            {
                var raceday = new RaceDay()
                {
                    Track = "TestTrack",
                    Date = "20180102"
                };

                DrfParser.ParseLine(context, raceday);
                DrfParser.ParseLine(context, raceday);

                Assert.AreEqual(1, context.RaceDays.Count());
                Assert.AreEqual("TestTrack", context.RaceDays.Single().Track);
                Assert.AreEqual("20180102", context.RaceDays.Single().Date);
            }
        }

        [Test]
        public void InsertMultipleRaceDaysTest()
        {
            using (var context = new HorseSenseContext(GetHorseSenseContextOptionForMethod()))
            {
                var raceday = new RaceDay()
                {
                    Track = "TestTrack",
                    Date = "20180102"
                };
                var racedayNewDate = new RaceDay()
                {
                    Track = "TestTrack",
                    Date = "20180103"
                };
                var racedayNewTrack = new RaceDay()
                {
                    Track = "AnotherTrack",
                    Date = "20180102"
                };
                DrfParser.ParseLine(context, raceday);
                DrfParser.ParseLine(context, racedayNewDate);
                DrfParser.ParseLine(context, racedayNewTrack);

                Assert.AreEqual(3, context.RaceDays.Count(),"Incorrect number of raceDays");
                Assert.IsNotNull(context.RaceDays.FirstOrDefault(r =>
                    r.Track == "TestTrack" && r.Date == "20180102"));
                Assert.AreEqual(1, context.RaceDays.Where(r =>
                    r.Track == "TestTrack" && r.Date == "20180103").Count());
                Assert.AreEqual(1, context.RaceDays.Where(r =>
                    r.Track == "AnotherTrack" && r.Date == "20180102").Count());
            }
        }
    }
}
