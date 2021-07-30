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
    public partial class FrmAddTool : Form
    {
        #region Variable Declaration
        private long _PKID = 0,_RentalId = 0;
        private DataTable _dtbAddTool = null;
        private bool NewTool = false;
        #endregion

        #region Constructors
        public FrmAddTool(string RentalId)
        {
            NewTool = true;
            _RentalId = long.Parse(RentalId);
            InitializeComponent();
            InitializeDataTable();
            
        }

        public FrmAddTool(long PKID)
        {
            _PKID = PKID;
            InitializeComponent();
            InitializeDataTable();
            
        }
        #endregion

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmAddTool_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }

        private void FrmAddTool_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
            if (NewTool)
            {
                txtRentalToolID.Text = _RentalId.ToString();
            }
           
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (NewTool)
            {
                txtRentalToolID.Focus();
                txtRentalToolID.Text = _RentalId.ToString();
                btnAdd.Focus();
            }
            //End Edit
            _dtbAddTool.Rows[0].EndEdit();
            //Save changes to the data table
            VRModel.Context.SaveDatabaseTable(_dtbAddTool);
        }
        #endregion

        #region Accessor

        private void InitializeDataTable()
        {
            string Query = $"SELECT * FROM RentalList WHERE RentalListId = {_PKID}";
            _dtbAddTool = VRModel.Context.GetDataTable(Query,"RentalList");
             if (NewTool)
            {
                DataRow row = _dtbAddTool.NewRow();
                row["RentalId"] = -999;
                _dtbAddTool.Rows.Add(row);
            }
            _dtbAddTool.Rows[0].BeginEdit();
        }
 
        #endregion

        #region Helper Methods
        /// <summary>
        /// This method will populate the combo box
        /// </summary>
        private void PopulateComboBox()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Inventory");
            cboTool.DataSource = dtb;
            cboTool.ValueMember = "ToolId";
            cboTool.DisplayMember = "Description";
        }

      

        /// <summary>
        /// This method will bind the cbotool to the ToolId field
        /// </summary>
        private void BindControls()
        {
            txtRentalToolID.DataBindings.Add("Text",_dtbAddTool,"RentalId");
            cboTool.DataBindings.Add("SelectedValue",_dtbAddTool,"ToolId");
            dtpCheckOut.DataBindings.Add("Text",_dtbAddTool,"DayCheckedOUT");
            dtpCheckIn.DataBindings.Add("Text",_dtbAddTool,"DayCheckedIN");

            if (string.IsNullOrEmpty(_dtbAddTool.Rows[0]["DayCheckedIN"].ToString()))
            {
                dtpCheckIn.Format = DateTimePickerFormat.Custom;
                dtpCheckIn.CustomFormat = " ";
            }

            if (NewTool)
                _dtbAddTool.Rows[0]["DayCheckedOUT"] = DateTime.Today;
        }

        #endregion

       
    }
}
