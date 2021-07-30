using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_ItemsRental
{
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckedOut_Click(object sender, EventArgs e)
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            string Query = "SELECT ToolId, Assetnumber, Description, Brand, Comments FROM Inventory Where CheckedOut = 'Yes'";
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable(Query, "Inventory");
            ToCSV(dtb, "AllCheckedOutTools.csv");
            
        }
        private void btnActiveToolByBrand_Click(object sender, EventArgs e)
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            string Query = "SELECT  ToolId,Brand , Assetnumber, Description, CheckedOut, Comments, ActiveRetired " +
                           "FROM Inventory " +
                           "WHERE ActiveRetired = 'Active'" +
                           "ORDER BY Brand ASC";

            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable(Query, "Inventory");
            ToCSV(dtb, "AllActiveToolsByBrand.csv");
        }

        private void btnRetiredTool_Click(object sender, EventArgs e)
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            string Query = "SELECT ToolId, Assetnumber, Description, Brand, CheckedOut, Comments, ActiveRetired " +
                           "FROM Inventory " +
                           "WHERE ActiveRetired = 'Retired'";
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable(Query, "Inventory");
            ToCSV(dtb, "AllRetiredTools.csv");
        }

        private void btnRetiredToolsByBrand_Click(object sender, EventArgs e)
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            string Query = "SELECT  ToolId,Brand , Assetnumber, Description, CheckedOut, Comments, ActiveRetired " +
                           "FROM Inventory " +
                           "WHERE ActiveRetired = 'Retired'" +
                           "ORDER BY Brand ASC";

            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable(Query, "Inventory");
            ToCSV(dtb, "AllRetiredToolsByBrand.csv");
        }
        private void FrmReport_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }
        #endregion

        #region Helper Method
        /// <summary>
        /// This Method will create a csv file contain data in a data table
        /// </summary>
        /// <param name="dtDataTable">Datatable needed to export</param>
        /// <param name="FileName">Name of the file need to export</param>
        public void ToCSV( DataTable dtDataTable,string FileName)
        {   
            StreamWriter sw = new StreamWriter(FileName, false);
            foreach (DataRow row in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        string value = row[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(row[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        private void btnActiveTool_Click(object sender, EventArgs e)
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            string Query = "SELECT ToolId, Assetnumber, Description, Brand, CheckedOut, Comments, ActiveRetired " +
                           "FROM Inventory " +
                           "WHERE ActiveRetired = 'Active'";
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable(Query, "Inventory");
            ToCSV(dtb, "AllActiveTools.csv");
        }

        #endregion

       
    }
}
