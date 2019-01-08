using HorseSense_AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorseSenseTest
{
    public class HorseSenseTestContext : HorseSenseContext
    {

        // Each test method specifies a unique database name, meaning each method has its own InMemory database.
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory

        public HorseSenseTestContext(                        
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
            : base(new DbContextOptionsBuilder<HorseSenseContext>()
                .UseInMemoryDatabase(databaseName: callerName).Options)
        {
        }
    }
}
