using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSqlQuery.Tests.TestDbRepository
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestEntity> Tests { get; set; }

        public TestDbContext()
        {
            
        }
    }
}
