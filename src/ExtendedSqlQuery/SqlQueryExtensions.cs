using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSqlQuery
{
    public static class SqlQueryExtensions
    {
        public static DbRawSqlQuery<TElement> ExtendedSqlQuery<TElement>(this Database db, string sql, params object[] parameters)
        {
            var listParameters = parameters.Where(n => n.GetType() == typeof(SqlListParameter)).ToArray();

            sql = listParameters.Aggregate(sql, (dbSql, parameter) => ApplyList(dbSql, parameter as SqlListParameter));

            parameters = parameters.Where(n => n.GetType() != typeof(SqlListParameter)).ToArray();

            return db.SqlQuery<TElement>(sql, parameters);
        }

        private static string ApplyList(string sql, SqlListParameter parameter)
        {
            var list = parameter.Value as IEnumerable<int>;

            if (list == null)
                throw new SqlListException("SqlListParameter value should has could be casted to IEnumerable<int>");

            var joinedListItems = string.Join(",", list);

            return sql.Replace(parameter.ParameterName, joinedListItems);
        }
    }
}
