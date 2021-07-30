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
    public partial class frmTools : Form
    {
        #region Variables Declaration

        private DataTable _dtbInventory = null;
        private long _PrKID = 0;
        private bool NewTool = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor - create new record
        /// </summary>
        public frmTools()
        {
            NewTool = true;
            InitializeComponent();
            InitializeDataTable();
        }

        /// <summary>
        /// Consturctor - open and update existing record
        /// </summary>
        /// <param name="PKID"></param>
        public frmTools(long PKID)
        {
            InitializeComponent();
            _PrKID = PKID;
            InitializeDataTable();
        }

        #endregion
        
        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNewTools_Load(object sender, EventArgs e)
        {
            PopulateBrandComboBox();
            BindControls();
            if (NewTool)
            {
                txtToolID.Text = _PrKID.ToString();
            }
            
        }

        private void llblnewBrand_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBrand frm = new FrmBrand();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateBrandComboBox();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //EndEdit
            _dtbInventory.Rows[0].EndEdit();
            //Save the cahnges to the data table
            VRModel.Context.SaveDatabaseTable(_dtbInventory);
           
        }
        private void frmTools_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }

        #endregion

        #region Accessor 

        private void InitializeDataTable()
        {
            string Query = $"SELECT * FROM Inventory WHERE ToolId = {_PrKID}";
            _dtbInventory = VRModel.Context.GetDataTable(Query,"Inventory");

            if (NewTool == true)
            {
                DataRow row = _dtbInventory.NewRow();
                _dtbInventory.Rows.Add(row);
            }
        }
        #endregion

        #region Helper Method

        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            // Binding the textbox txtMovieId with the data table '_dtbInventory' and map it to the database field called 'ToolId' and use the
            // 'Text' property of the control for binding.
            txtToolID.DataBindings.Add("Text", _dtbInventory, "ToolId");
            txtAssetNumber.DataBindings.Add("Text", _dtbInventory, "AssetNumber");
            txtDesc.DataBindings.Add("Text",_dtbInventory,"Description");
            cboBrand.DataBindings.Add("Text",_dtbInventory,"Brand");
            cboActiveRetired.DataBindings.Add("Text", _dtbInventory, "ActiveRetired");
            cboCheckedOUT.DataBindings.Add("Text", _dtbInventory, "CheckedOut");
            txtComment.DataBindings.Add("Text", _dtbInventory, "Comments");
        }

        private void PopulateBrandComboBox()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Brand");
            // The ValueMember is the primary key of the source table.  This is the value that will be stored in the database when 
            // a user selects a row from the combo box.
            cboBrand.ValueMember = "BrandId";
            // The DisplayMember is the field name of the source table that we want to display in the combo box.
            cboBrand.DisplayMember = "Brand";
            // Set the data source of the combo box by using the data table returned by the GetDataTable of Context.
            cboBrand.DataSource = dtb;
        }



        #endregion

       
    }
}
