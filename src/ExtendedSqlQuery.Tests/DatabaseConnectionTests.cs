using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSqlQuery.Tests.TestDbRepository;
using FluentAssertions;
using NUnit.Framework;

namespace ExtendedSqlQuery.Tests
{
    public class DatabaseConnectionTests
    {
        [Test]
        public void When_database_not_exists_Should_create_it()
        {
            using (var context = new TestDbContext())
            {
                context.Database.CreateIfNotExists();

                var isDbExists = context.Database.Exists();

                isDbExists.Should().BeTrue("because it's necessary to further tests");
            }
        }

        [Test]
        public void When_database_is_empty_Should_seed_it_with_data()
        {
            using (var context = new TestDbContext())
            {
                if (!context.Tests.Any())
                {
                    context.Tests.AddRange(new [] { new TestEntity() {Name = "Test1"}, new TestEntity() { Name = "Test2"} });
                    context.SaveChanges();
                }


                context.Tests.Any().Should().BeTrue("because test data is needed to tests");
            }
        }


    }
}
