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
    public partial class FrmNewCustomer : Form
    {
        #region Variables Declaration

        private DataTable _dtbCustomer = null;
        private long _PrKID = 0;
        private bool NewCustomer = false;
        
        #endregion

        #region Constructor

        public FrmNewCustomer()
        {
            NewCustomer = true;
            InitializeComponent();
            InitializeDataTable();
        }

        public FrmNewCustomer(long PKID)
        {
            InitializeComponent();
            _PrKID = PKID;
            InitializeDataTable();
        }
        
        #endregion

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNewCustomer_Load(object sender, EventArgs e)
        {
            BindControls();
            if (NewCustomer)
            {
                txtID.Text = _PrKID.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _dtbCustomer.Rows[0].EndEdit();
            VRModel.Context.SaveDatabaseTable(_dtbCustomer);
        }
        private void FrmNewCustomer_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }

        #endregion

        #region Accessor 

        private void InitializeDataTable()
        {
            string Query = $"SELECT * FROM Customer WHERE CustomerId = {_PrKID}";
            _dtbCustomer = VRModel.Context.GetDataTable(Query, "Customer");

            if (NewCustomer == true)
            {
                DataRow row = _dtbCustomer.NewRow();
                _dtbCustomer.Rows.Add(row);
            }
        }
        #endregion

        #region Helper Method

        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            txtID.DataBindings.Add("Text",_dtbCustomer,"CustomerId");
            txtName.DataBindings.Add("Text", _dtbCustomer, "CustomerName");
            txtNumber.DataBindings.Add("Text", _dtbCustomer, "Phone");
        }




        #endregion

       
    }
}
