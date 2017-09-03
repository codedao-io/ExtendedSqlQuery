using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSqlQuery
{
    public class SqlListParameter : DbParameter
    {
        private string _internalName;
        protected string InternalName
        {
            get => _internalName;
            set => _internalName = NormalizeParameterName(value);
        }

        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; }

        public override string SourceColumn { get; set; }
        public override object Value { get; set; }
        public override bool SourceColumnNullMapping { get; set; }
        public override int Size { get; set; }

        public override string ParameterName
        {
            get => InternalName;
            set => InternalName = value;
        }

        public override void ResetDbType()
        {

        }

        public SqlListParameter(string parameterName)
        {
            InternalName = parameterName;
        }

        internal static string NormalizeParameterName(string parameterName)
        {
            return string.IsNullOrWhiteSpace(parameterName) || parameterName.First() == '@'
                ? parameterName
                : "@" + parameterName;
        }
    }

}
