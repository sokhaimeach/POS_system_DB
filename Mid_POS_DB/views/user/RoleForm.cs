using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mid_POS_DB.models;

namespace Mid_POS_DB.views.user
{
    public partial class RoleForm : Form
    {
        public RoleForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            Role role = new Role();
            role.GetData(dgRole);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search role")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search role";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.Name = txtRolename.Text.Trim();
            role.Status = rTrue.Checked ? true : false;
            role.Create();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.DeleteById(dgRole);
        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Role role = new Role();
            role.TransferDataToControl(dgRole, txtRolename, rTrue, rFalse);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.Name= txtRolename.Text.Trim();
            role.Status = rTrue.Checked? true : false;
            role.UpdateById(dgRole);
            role.GetData(dgRole);
        }
    }
}
