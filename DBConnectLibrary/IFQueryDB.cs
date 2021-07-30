using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectLibrary
{
    interface IFQueryDB
    {
        DataTable GetDataTable(string _TableName);

        DataTable GetDataTable(string _TableName, bool _IsReadOnly);

        DataTable GetDataTable(string _SQLQuery, string _TableName);

        DataTable GetDataTable(string _SQLQuery, string _TableName, bool _IsReadOnly);
    }
}
