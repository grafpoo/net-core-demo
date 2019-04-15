using System;
using MySql.Data.Entity;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;


namespace insignia
{
    
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TestContext : DbContext
    {
        public TestContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<TestData> TestData { get; set; }
    }
}
