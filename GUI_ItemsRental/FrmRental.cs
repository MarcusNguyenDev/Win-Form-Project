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
    public partial class FrmRental : Form
    {
        #region Events

        public FrmRental()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRental_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmNewRental frm = new FrmNewRental();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }
        }

        private void dgvRental_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRental.CurrentCell == null) return;

            long PKID = long.Parse(dgvRental[0, dgvRental.CurrentCell.RowIndex].Value.ToString());
            FrmNewRental frm = new FrmNewRental(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }
        }
        private void FrmRental_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }
      
        #endregion

        #region Helper Methods
        /// <summary>
        /// Populate the grid
        /// </summary>
        private void PopulateGrid()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Rental");
            dgvRental.DataSource = dtb;
        }






        #endregion

        
    }
}
