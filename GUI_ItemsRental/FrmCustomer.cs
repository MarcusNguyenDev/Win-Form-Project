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
    public partial class FrmCustomer : Form

    #region Events
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmNewCustomer frm = new FrmNewCustomer();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
            }
        }
        private void dgvCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvCustomer.CurrentCell == null) return;

            long PKID = long.Parse(dgvCustomer[0, dgvCustomer.CurrentCell.RowIndex].Value.ToString());
            FrmNewCustomer frm = new FrmNewCustomer(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }
        private void FrmCustomer_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Properties.Settings.Default.Paint;
        }
     

        #endregion

    #region Helper Methods

        private void PopulateGrid()
        {
            VRModel.Context.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtb = new DataTable();
            dtb = VRModel.Context.GetDataTable("Customer");
            dgvCustomer.DataSource = dtb;
        }





        #endregion

        
    }


}
