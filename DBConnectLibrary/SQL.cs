using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectLibrary
{
    public class SQL : IFAlterDB, IFQueryDB
    {
        #region Variable Declarations

        public string ConnectionString { get; set; }

        #endregion

        #region Mutators

        /// <summary>
        /// This method will alter the spectified database table on a specified server and database
        /// </summary>
        /// <param name="ServerName">Destination Server</param>
        /// <param name="DatabaseName">Destination Database</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="TableStructure">Table Structure</param>
        public void AlterDatabaseTable(string ServerName, string DatabaseName, string TableName, string TableStructure)
        {
            try
            {
                ConnectionString = $"Data Source={ServerName}; Initial Catalog={DatabaseName}; Integrated Security=True";
                string Query = $"ALTER TABLE {TableName} ({TableStructure})";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// This method will create a database on a specified server
        /// </summary>
        /// <param name="ServerName">Destination Server</param>
        /// <param name="DatabaseName">Database Name</param>
        public void CreateDatabase(string ServerName, string DatabaseName)
        {
            try
            {
                if (IsDatabaseExists(ServerName, DatabaseName)) return;

                ConnectionString = $"Data Source={ServerName}; " +
                                          $"Integrated Security=True";
                string sqlQuery = $"CREATE DATABASE {DatabaseName}";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// This method will create a database table on a specified server
        /// and database.
        /// </summary>
        /// <param name="ServerName">Destination Server</param>
        /// <param name="DatabaseName">Destination Database</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="TableStructure">Table Structure</param>
        public void CreateDatabaseTable(string ServerName, string DatabaseName, string TableName, string TableStructure)
        {
            try
            {
                ConnectionString = $"Data Source={ServerName}; " +
                                          $"Initial Catalog={DatabaseName};" +
                                          $"Integrated Security=True";

                string sqlQuery = $"IF NOT EXISTS (SELECT name FROM sysobjects " +
                                  $"WHERE name = '{TableName}') " +
                                  $"CREATE TABLE {TableName} ({TableStructure})";
                Console.WriteLine(sqlQuery);

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// This method will delete a record in the database
        /// </summary>
        /// <param name="ConnectionString">Connection String</param>
        /// <param name="TableName">Table name</param>
        /// <param name="PKName">Primary Key Name</param>
        /// <param name="PKID">Primary Key ID</param>
        /// <returns></returns>
        public int DeleteRecord(string ConnectionString, string TableName, string PKName, string PKID)
        {
            int Id = 0;

            string Query = $"DELETE FROM {TableName} WHERE {PKName} = {PKID}";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Id;
        }

        #endregion

        #region Accessors

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="TableName">Source Table</param>
        /// <returns>Datatable</returns>
        public DataTable GetDataTable(string _TableName)
        {
            DataTable Table = new DataTable
            {
                TableName = _TableName
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {_TableName}", connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        adapter.Fill(Table);
                        connection.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return Table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="TableName">Source Table</param>
        /// <param name="IsReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string _TableName, bool _IsReadOnly)
        {
            if (_IsReadOnly == false) return GetDataTable(_TableName);

            DataTable Table = new DataTable
            {
                TableName = _TableName
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT * FROM {_TableName}", connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            connection.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Table;
        }

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="_SQLQuery">SQL query to retrieve records.</param>
        /// <param name="_TableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string _SQLQuery, string _TableName)
        {
            DataTable Table = new DataTable
            {
                TableName = _TableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(_SQLQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Fill(Table);
                        conn.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Table;
        }
        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="strSQLQuery">SQL query to retrieve records.</param>
        /// <param name="strTableName">Source Table<</param>
        /// <param name="blnIsReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string _SQLQuery, string _TableName, bool _IsReadOnly)
        {
            if (_IsReadOnly == false) return GetDataTable(_SQLQuery, _TableName);

            DataTable Table = new DataTable
            {
                TableName = _TableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(_SQLQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            conn.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Table;
        }

        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="ConnectionString">Connection String</param>
        /// <param name="TableName">Destination Table</param>
        /// <param name="ColumnNames">Column Names of the table</param>
        /// <param name="ColumnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertParentRecord(string ConnectionString, string TableName, string ColumnNames, string ColumnValues)
        {
            int Id = 0;

            try
            {
                string Query = $"INSERT INTO {TableName} ({ColumnNames}) VALUES ({ColumnValues}) SELECT SCOPE_IDENTITY()";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Id;
        }

        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="ServerName">Destination Server</param>
        /// <param name="DatabaseName">Destination Database</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="ColumnNames">Column Names of the table</param>
        /// <param name="ColumnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertRecord(string ServerName, string DatabaseName, string TableName, string ColumnNames, string ColumnValues)
        {
            int Id = 0;
            ConnectionString = $"Data Source={ServerName}; Initial Catalog={DatabaseName}; Integrated Security=True";

            string Query = $"SET IDENTITY_INSERT {TableName} ON INSERT INTO {TableName} ({ColumnNames}) VALUES ({ColumnValues}) SET IDENTITY_INSERT {TableName} OFF";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        Id = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Id;
        }

        ///<summary>
        /// This method will update a database table.
        /// </summary>
        /// <param name="Table">Source Table</param>
        public void SaveDatabaseTable(DataTable Table)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {Table.TableName}", connection))
                    {
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        if (connection.State == ConnectionState.Closed) connection.Open();
                        adapter.Update(Table);
                        connection.Close();
                        Table.AcceptChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// This method will update a record in the database.
        /// </summary>
        /// <param name="ServerName">Destination Server</param>
        /// <param name="DatabaseName">Destination Database</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="ColumnNameAndValues">Column Names and corresponding values</param>
        /// <param name="Criteria">Update Criteria</param>
        /// <returns>bool IsOk</returns>
        public bool UpdateRecord(string ServerName, string DatabaseName, string TableName, string ColumnNameAndValues, string Criteria)
        {
            bool IsOK = false;

            ConnectionString = $"Data Source={ServerName}; Initital Catalog={DatabaseName}; Integrated Sercurity=True";
            string Query = $"UPDATE {DatabaseName} SET {ColumnNameAndValues} WHERE {Criteria}";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        command.ExecuteNonQuery();
                        IsOK = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                IsOK = false;
                throw;
            }
            return IsOK;
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// This method will check if the specified database table exists in the specified database on a specified database server
        /// </summary>
        /// <param name="ServerName">Source Database Server</param>
        /// <param name="DatabaseName">Source Database Name</param>
        /// <param name="TableName">Table Name to check</param>
        /// <returns>bool</returns>
        public bool IsDatabaseTableExists(string ServerName, string DatabaseName, string TableName)
        {
            bool IsExists = false;
            ConnectionString = $"Data Source={ServerName}; Initial Catalog={DatabaseName}; Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT 1 FORM {TableName}) WHERE 1=0", connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        command.ExecuteScalar();
                        IsExists = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                IsExists = false;
            }

            return IsExists;
        }

        public bool IsDatabaseExists(string ServerName, string DatabaseName)
        {
            ConnectionString = $"Data Source={ServerName}; Integrated Security=True";
            string Query = $"SELECT database_id FROM sys.databases WHERE Name = '{DatabaseName}'";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();
                        object objresult = command.ExecuteScalar();
                        connection.Close();
                        return (objresult != null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return true;
        }


        #endregion
    }
}

