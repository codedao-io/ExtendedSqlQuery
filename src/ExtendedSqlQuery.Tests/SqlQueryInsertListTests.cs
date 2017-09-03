using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSqlQuery.Tests.TestDbRepository;
using FluentAssertions;
using NUnit.Framework;

namespace ExtendedSqlQuery.Tests
{
    public class SqlQueryInsertListTests
    {
        [Test]
        public void When_select_by_idents_list_Should_get_results()
        {
            using (var context = new TestDbContext())
            {
                var parameters = new object[]
                {
                    new SqlListParameter("@identsList") { Value = new [] { 1, 2 }}, 
                    new SqlParameter("@nameParam", SqlDbType.VarChar) { Value = "Test1"}, 
                };

                var isFoundEntry = context.Database.ExtendedSqlQuery<TestEntity>("select * from TestEntities where Id in (@identsList) and name=@nameParam", parameters).Any();               

                isFoundEntry.Should().BeTrue("becasue entry with this params exists in database");
            }
        }

        [Test]
        public void When_list_parameter_is_not_enumerable_integers_Should_throw_excepion()
        {
            using (var context = new TestDbContext())
            {
                var parameters = new object[]
                {
                    new SqlListParameter("@identsList") { Value = "1, 2" },
                    new SqlParameter("@nameParam", SqlDbType.VarChar) { Value = "Test1"},
                };

                context.Database
                    .Invoking(n => n.ExtendedSqlQuery<TestEntity>("select * from TestEntities where Id in (@identsList) and name=@nameParam", parameters).Any())
                    .ShouldThrow<SqlListException>()
                    .WithMessage("SqlListParameter value should has could be casted to IEnumerable<int>");
            }
        }
    }
}
