using DBConnectLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRModel
{
    public class Context
    {
        #region Variable Declarations

        public static string  ConnectionString { get; set; }
        private static SQL _sql = new SQL();
        #endregion

        #region Accessors

        public static DataTable GetDataTable(string TableName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.GetDataTable(TableName);
        }

        public static DataTable GetDataTable(string Query, string TableName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.GetDataTable(Query, TableName);
        }
        #endregion

        #region Mutators

        public static void SaveDatabaseTable(DataTable Table)
        {
            _sql.ConnectionString = ConnectionString;
            _sql.SaveDatabaseTable(Table);
        }

        public static int InsertParentTable(string TableName, string ColumnNames, string ColumnValues)
        {
            return _sql.InsertParentRecord(ConnectionString, TableName, ColumnNames,
                                           ColumnValues);
        }

        public static int DeleteRecord(string strTableName, string strPKName,string strPKID)
        {
            return _sql.DeleteRecord(ConnectionString, strTableName, strPKName, strPKID);
        }

        public static int InsertRecord(string ServerName, string DatabaseName, string TableName, string ColumnNames, string ColumnValues)
        {
            return _sql.InsertRecord(ServerName,DatabaseName,TableName,ColumnNames,ColumnValues);
        }
        #endregion
    }
}
