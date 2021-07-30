using DBConnectLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRModel
{
    public class Initializer
    {// Make this a static class so we can call it without creating an instance of it

        //TODO: Replace the ServerName with the name of your database instance
        private static string _ServerName = @"DESKTOP-2F5U7JB\SQLEXPRESS", _DatabaseName = "YourProjectCW";
        static SQL _sql = new SQL();

        /// <summary>
        /// This method will create the database on the specified SQL Server
        /// </summary>
        public static void CreateDatabase()
        {
            // Create the Database
            _sql.CreateDatabase($"{_ServerName}", $"{_DatabaseName}");
            CreateDatabaseTables();
            SeedDatabase();

        }

        /// <summary>
        /// This method will create the database tables required on SQL server
        /// </summary>
        private static void CreateDatabaseTables()
        {
            string connectionString = $"Data Source={_ServerName}; Initial Catalog={_DatabaseName}; Integrated Security=True";


            // Create Customer table
            // Each line represent a column
            string CustomerDataStructure = "CustomerId int IDENTITY(1,1) PRIMARY KEY, " +
                                           "CustomerName VARCHAR(70) NOT NULL, " +
                                           "Phone VARCHAR(20) NOT NULL";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Customer"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Customer", CustomerDataStructure);
            }

            // Create Workspace table
            // Each line represent a column
            string WorkspaceDataStructure = "WorkspaceId int IDENTITY(1,1) PRIMARY KEY, " +
                                            "Workspace VARCHAR(100) NOT NULL";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Workspace"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Workspace", WorkspaceDataStructure);
            }

            //create Brand table
            // Each line represent a column
            string BrandDataStructure = "BrandId int IDENTITY(1,1) PRIMARY KEY, " +
                                        "Brand VARCHAR(100) NOT NULL";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Brand"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Brand", BrandDataStructure);
            }

            // Create Iventory table
            // Each line represent a column
            string IventoryDataStructure = "ToolId int IDENTITY(1,1) PRIMARY KEY," +
                                           "Assetnumber int NOT NULL UNIQUE, " +
                                           "Description VARCHAR(100), " +
                                           "Brand VARCHAR(100) NOT NULL, " +
                                           "ActiveRetired VARCHAR(10) NOT NULL, " +
                                           "CheckedOut VARCHAR(3) NOT NULL, " +
                                           "Comments VARCHAR(100)";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Inventory"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Inventory", IventoryDataStructure);
            }


            // Create Rental table
            // Each line represent a column
            string RentalDataStructure = "RentalId int IDENTITY(1,1) PRIMARY KEY, " +
                                         "CustomerId int NOT NULL, " +
                                         "Workspace VARCHAR(100) NOT NULL, " +
                                         "DateRented DATETIME NOT NULL, " +
                                         "DateReturned DATETIME NULL";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Rental"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Rental", RentalDataStructure);
            }


            // Create RentalList table
            // Each line represent a column
            string RentalListDataStructure = "RentalListId int IDENTITY(1,1) PRIMARY KEY, " +
                                             "RentalId int NOT NULL," +
                                             "ToolId int NOT NULL, " +
                                             "DaycheckedOUT DATE NOT NULL, " +
                                             "DaycheckedIN DATE NULL";
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "RentalList"))
            {
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "RentalList", RentalListDataStructure);
            }
        }

        public static void SeedDatabase()
        {
            SeedBrand();
            SeedInventory();
            SeedCustomer();
            SeedWorkspace();
            SeedRental();
            SeedRentalList();
        }

        private static void SeedInventory()
        {
            List<string> inventory = new List<string>
            {
                "1,1,'This is Description no.1','Brand no.1','Active','Yes','This is Comment Number 1'",
                "2,2,'This is Description no.2','Brand no.1','Active','Yes','This is Comment Number 2'",
                "3,3,'This is Description no.3','Brand no.1','Active','Yes','This is Comment Number 3'",
                "4,4,'This is Description no.4','Brand no.1','Active','Yes','This is Comment Number 4'",
                "5,5,'This is Description no.5','Brand no.2','Active','Yes','This is Comment Number 5'",
                "6,6,'This is Description no.6','Brand no.2','Active','No','This is Comment Number 6'",
                "7,7,'This is Description no.7','Brand no.3','Active','No','This is Comment Number 7'",
            };

            foreach (var tool in inventory)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Inventory", "ToolId,Assetnumber,Description,Brand,ActiveRetired,CheckedOut,Comments", tool);
            }
        }

        private static void SeedBrand()
        {
            List<string> brands = new List<string>
            {
                "1,'Brand no.1'",
                "2,'Brand no.2'",
                "3,'Brand no.3'",
                "4,'Brand no.4'",
                "5,'Brand no.5'",
                "6,'Brand no.6'",
            };

            foreach (var brand in brands)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Brand", "BrandId, Brand", brand);
            }
        }

        private static void SeedCustomer()
        {

            List<string> customer = new List<string>
            {
                "1,'Customer no.1','123456789'",
                "2,'Customer no.2','123456789'",
                "3,'Customer no.3','123456789'",
                "4,'Customer no.4','123456789'",
                "5,'Customer no.5','123456789'",
                "6,'Customer no.6','123456789'",
            };

            foreach (var ctm in customer)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Customer", "CustomerId, CustomerName , Phone", ctm);
            }
        }

        private static void SeedWorkspace()
        {
            List<string> Workspace = new List<string>
            {
                "1,'WorkSpace no.1'",
                "2,'WorkSpace no.2'",
                "3,'WorkSpace no.3'",
                "4,'WorkSpace no.4'",
                "5,'WorkSpace no.5'",
                "6,'WorkSpace no.6'",
            };

            foreach (var ws in Workspace)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Workspace", "WorkspaceId,Workspace", ws);
            }
        }

        private static void SeedRental()
        {
            List<string> rental = new List<string>
            {
                $"1,1,'WorkSpace no.1','{DateTime.Parse("01/02/2013")}',null",
                $"2,2,'WorkSpace no.1','{DateTime.Parse("01/02/2013")}',null",
                $"3,3,'WorkSpace no.3','{DateTime.Parse("02/03/2013")}',null",
                $"4,3,'WorkSpace no.2','{DateTime.Parse("03/04/2013")}',null",
                $"5,4,'WorkSpace no.1','{DateTime.Parse("02/01/2013")}',null",
                $"6,4,'WorkSpace no.1','{DateTime.Parse("01/01/2013")}',null",
            };

            foreach (var rt in rental)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Rental", "RentalId,CustomerId,Workspace,DateRented,DateReturned", rt);
            }
        }

        private static void SeedRentalList()
        {
            List<string> rental = new List<string>
            {
                $"1,1,1,'{DateTime.Parse("01/02/2013")}',null",
                $"2,2,3,'{DateTime.Parse("01/02/2013")}',null",
                $"3,3,2,'{DateTime.Parse("02/03/2013")}',null",
                $"4,4,4,'{DateTime.Parse("03/04/2013")}',null",
                $"5,5,5,'{DateTime.Parse("02/01/2013")}',null",
                $"6,6,6,'{DateTime.Parse("01/01/2013")}',null",
                $"7,2,3,'{DateTime.Parse("01/02/2013")}',null",
                $"8,3,2,'{DateTime.Parse("02/03/2013")}',null",
                $"9,4,4,'{DateTime.Parse("03/04/2013")}',null",
                $"10,5,5,'{DateTime.Parse("02/01/2013")}',null",
                $"11,6,6,'{DateTime.Parse("01/01/2013")}',null",
                $"12,1,1,'{DateTime.Parse("01/02/2013")}',null",
                $"13,2,3,'{DateTime.Parse("01/02/2013")}',null",
                $"14,3,2,'{DateTime.Parse("02/03/2013")}',null",
                $"15,4,4,'{DateTime.Parse("03/04/2013")}',null",
                $"16,5,5,'{DateTime.Parse("02/01/2013")}',null",
                $"17,6,6,'{DateTime.Parse("01/01/2013")}',null",
                $"18,2,3,'{DateTime.Parse("01/02/2013")}',null",
                $"19,3,2,'{DateTime.Parse("02/03/2013")}',null",
                $"20,4,4,'{DateTime.Parse("03/04/2013")}',null",
                $"21,5,5,'{DateTime.Parse("02/01/2013")}',null",
                $"22,6,6,'{DateTime.Parse("01/01/2013")}',null",
            };

            foreach (var rt in rental)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "RentalList", "RentalListId,RentalId,ToolId,DaycheckedOUT,DaycheckedIN", rt);
            }
        }
    }
    

}
