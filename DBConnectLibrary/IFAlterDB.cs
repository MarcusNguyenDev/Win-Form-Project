using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectLibrary
{
    interface IFAlterDB
    {
        void SaveDatabaseTable(DataTable Table);

        void CreateDatabase(string ServerName, string DatabaseName);

        void CreateDatabaseTable(string ServerName, string DatabaseName, string TableName, string TableStructure);

        void AlterDatabaseTable(string ServerName, string DatabaseName, string TableName, string TableStructure);

        int InsertRecord(string ServerName, string DatabaseName, string TableName, string ColumnNames, string ColumnValues);

        int InsertParentRecord(string ConnectionString, string TableName, string ColumnNames, string ColumnValues);

        bool UpdateRecord(string ServerName, string DatabaseName, string TableName, string ColumnNameAndValues, string Criteria);

        int DeleteRecord(string ConnectionString, string TableName, string PKName, string PKID);
    }
}

