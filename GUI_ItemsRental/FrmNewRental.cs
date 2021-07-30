using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_ItemsRental
{
    public partial class FrmNewRental : Form
    {
        #region Variable Declarations

        private long _lngPKID = 0;
        private DataTable _dtbRental = null, _dtbRentalList = null;
        private bool NewRental = false;

        #endregion

        #region Constructor

        public FrmNewRental()
        {
            NewRental = true;
            InitializeComponent();
            InitializeDataTable();
           
        }

        public FrmNewRental(long PKID)
        {
            InitializeComponent();
            _lngPKID = PKID;
            InitializeDataTable();
        }
        #endregion

        #region Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //EndEdit
            if (_dtbRental.Rows.Count > 0)
            {
                _dtbRental.Rows[0]["DateRented"] = dtpDateRented.Value.ToString("yyyy-MM-dd");
                _dtbRental.Rows[0].EndEdit();
            }
            //Save the cahnges to the data table
            VRModel.Context.SaveDatabaseTable(_dtbRental);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (NewRental && _lngPKID <= 0)
            {
                string ColumnNames = "Workspace, CustomerId, DateRented, DateReturned";
                // SQL accepts in the format of "yyyy-MM-dd"
                string DateRented = dtpDateRented.Value.ToString("yyyy-MM-dd");
                string ColumnValues = $"'{cboWorkSpace.Text}',{long.Parse(cboCustomer.SelectedValue.ToString())}, '{DateRented}', null";
                // The InsertParentTable will return the new PKID of the inserted record and store it in txtRentalId
                txtRentalId.Text = VRModel.Context.InsertParentTable("Rental", ColumnNames, ColumnValues).ToString();
                _lngPKID = long.Parse(txtRentalId.Text);
                InitializeDataTable();
            }

            FrmAddTool frm = new FrmAddTool(txtRentalId.Text);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }

        }

        private void FrmNewRental_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            PopulateWorkSpaceComboBox();
            BindControls();
            
            
            if (NewRental)
            {
                txtRentalId.Text = _lngPKID.ToString();
            }
        }

        private void dgvRentalList_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRentalList.CurrentCell == null) return;

            long PKID = long.Parse(dgvRentalList[0, dgvRentalList.CurrentCell.RowIndex].Value.ToString());
            FrmAddTool frm = new FrmAddTool(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }
        }
        private void FrmNewRental_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }

        #endregion

        #region Accessors

        private void InitializeDataTable()
        {
            string Query = $"SELECT * FROM Rental WHERE RentalId = {_lngPKID}";
            _dtbRental = VRModel.Context.GetDataTable(Query, "Rental");
            PopulateGrid();

            //if (NewRental)
            //{
            //    DataRow row = _dtbRental.NewRow();
            //    _dtbRental.Rows.Add(row);
            //}
            if(_dtbRental.Rows.Count > 0)
                _dtbRental.Rows[0].BeginEdit();
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// This method will populate the grid
        /// </summary>
        private void PopulateGrid()
        {
            string SqlQuery = "SELECT RentalList.RentalListId, RentalList.ToolId, Inventory.Assetnumber, Inventory.Description, RentalList.DaycheckedOUT, RentalList.DaycheckedIN " +
                                "FROM RentalList INNER JOIN Inventory ON RentalList.ToolId = Inventory.ToolId " +
                                $"WHERE RentalList.RentalId = {_lngPKID} " +
                                $"ORDER BY RentalList.RentalListId ";
            _dtbRentalList = VRModel.Context.GetDataTable(SqlQuery, "RentalList");
            dgvRentalList.DataSource = _dtbRentalList;
        }
        
        private void BindControls()
        {
            // Binding the textbox txtRentalId with the data table '_dtbRental and map it to the database field called 'RentalId' and use the
            // 'Text' property of the control for binding.
            txtRentalId.DataBindings.Add("Text", _dtbRental, "RentalId");
            cboCustomer.DataBindings.Add("SelectedValue", _dtbRental, "CustomerId");
            dtpDateRented.DataBindings.Add("Text", _dtbRental, "DateRented");
            dtpDateReturned.DataBindings.Add("Text", _dtbRental, "DateReturned");
            cboWorkSpace.DataBindings.Add("Text",_dtbRental,"Workspace");

            //Return null for the Returned column
            if (NewRental || string.IsNullOrEmpty(_dtbRental.Rows[0]["DateReturned"].ToString()))
            {
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = " ";
            }

            if (NewRental)
            {
                cboWorkSpace.SelectedIndex = -1;
                cboCustomer.SelectedIndex = -1;
            }

        }

        



        /// <summary>
        /// This method will populate the combo box by calling the GetDataTable of the Video Context and passed the table name of
        /// the source table.
        /// </summary>
        private void PopulateComboBox()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Customer");
            // The ValueMember is the primary key of the source table.  This is the value that will be stored in the database when 
            // a user selects a row from the combo box.
            cboCustomer.ValueMember = "CustomerId";
            // The DisplayMember is the field name of the source table that we want to display in the combo box.
            cboCustomer.DisplayMember = "CustomerName";
            // Set the data source of the combo box by using the data table returned by the GetDataTable of VideoContext.
            cboCustomer.DataSource = dtb;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected item?", "VideoRental", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                long PKID = long.Parse(dgvRentalList[0, dgvRentalList.CurrentCell.RowIndex].Value.ToString());

                //Use the DeleteRecord method
                VRModel.Context.DeleteRecord("RentalList", "RentalListId", PKID.ToString());
                PopulateGrid();
            }
        }

        private void PopulateWorkSpaceComboBox()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Workspace");
            // The ValueMember is the primary key of the source table.  This is the value that will be stored in the database when 
            // a user selects a row from the combo box.
            cboWorkSpace.ValueMember = "WorkspaceId";
            // The DisplayMember is the field name of the source table that we want to display in the combo box.
            cboWorkSpace.DisplayMember = "Workspace";
            // Set the data source of the combo box by using the data table returned by the GetDataTable of VideoContext.
            cboWorkSpace.DataSource = dtb;
        }

        #endregion




    }
}
