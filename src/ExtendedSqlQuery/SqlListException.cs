using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSqlQuery
{
    public class SqlListException : Exception
    {
        public SqlListException(string message) : base(message)
        {
            
        }
    }
}
