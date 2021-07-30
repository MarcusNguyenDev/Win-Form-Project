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
    public partial class FrmBrand : Form
    {
        #region Variables Declaration

        private DataTable _dtbBrand = null;
        private long _PrKID = 0;
        private bool NewTool = false;
        #endregion

        #region Constructor

        public FrmBrand()
        {
            InitializeComponent();
            InitializeDataTable();
        }

        #endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBrand_Load(object sender, EventArgs e)
        {
            BindControls();
            txtBrandId.Text = _PrKID.ToString();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            _dtbBrand.Rows[0].EndEdit();
            VRModel.Context.SaveDatabaseTable(_dtbBrand);
        }
        private void FrmBrand_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }
        #endregion

        #region Accessor 

        private void InitializeDataTable()
        {
            string Query = $"SELECT * FROM Brand WHERE BrandId = {_PrKID}";
            _dtbBrand = VRModel.Context.GetDataTable(Query, "Brand");

                DataRow row = _dtbBrand.NewRow();
                _dtbBrand.Rows.Add(row);
            
        }
        #endregion

        #region Helper Methods

        private void BindControls()
        {
            txtBrandId.DataBindings.Add("Text",_dtbBrand,"BrandId");
            txtBrandName.DataBindings.Add("Text", _dtbBrand,"Brand");
        }


        #endregion


    }
}
