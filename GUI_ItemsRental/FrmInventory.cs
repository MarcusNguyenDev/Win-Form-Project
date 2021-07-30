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
    public partial class frmInventory : Form
    {
        #region Constructors

        public frmInventory()
        {
            InitializeComponent();
        }

        #endregion


        #region Form Event
        private void frmInventory_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        #endregion

        #region Events

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTools frm = new frmTools();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }
        }
        private void dgvInventory_DoubleClick(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentCell == null) return;

            long PKID = long.Parse(dgvInventory[0, dgvInventory.CurrentCell.RowIndex].Value.ToString());
            frmTools frm = new frmTools(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();

        }
        private void frmInventory_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }

        #endregion

        #region Helper Methods

        private void PopulateGrid()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Inventory");
            dgvInventory.DataSource = dtb;
        }



        #endregion

       
    }
}
