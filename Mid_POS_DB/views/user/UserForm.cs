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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            User user = new User();
            user.SetRoleName(cboRoleName);
            user.GetData(dgUser);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtUserName, txtPassword, txtEmail)) return;
            if (cboRoleName.Text == string.Empty) return;
            User user = new User();
            user.Name = txtUserName.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;
            user.Gender = cboGender.Text;
            user.Status = rTrue.Checked ? true : false;
            user.RoleId = user.GetRoleId(cboRoleName.Text.Trim());
            user.Create();
            user.GetData(dgUser);
            Library.ClearTextBox(txtUserName, txtPassword, txtEmail);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.DeleteById(dgUser);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                User user = new User();
                user.Name = txtSearch.Text;
                user.Search(dgUser);
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search User")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search User";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void dgUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            User user = new User();
            user.TransferDataToControl(dgUser, txtUserName, cboGender, txtPassword, txtEmail, rTrue, rFalse, cboRoleName);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtUserName, txtPassword, txtEmail)) return;
            if (cboRoleName.Text == string.Empty) return;
            User user = new User();
            user.Name = txtUserName.Text.Trim();
            user.Gender = cboGender.Text.Trim();
            user.Password = txtPassword.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.Status = rTrue.Checked ? true : false;
            user.RoleId = user.GetRoleId(cboRoleName.Text.Trim());
            user.UpdateById(dgUser);
            user.GetData(dgUser);
            Library.ClearTextBox(txtUserName, txtPassword, txtEmail);
        }
    }
}
