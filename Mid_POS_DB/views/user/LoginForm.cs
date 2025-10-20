using Mid_POS_DB.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_POS_DB
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Name = txtUsername.Text;
            user.Password = txtPassword.Text;
            user.Login(this);
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(txtUsername.Text == String.Empty)
                {
                    txtUsername.Focus();
                } else
                {
                    txtPassword.Focus();
                }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(txtPassword.Text == String.Empty)
                {
                    txtPassword.Focus();
                } else if (txtUsername.Text == String.Empty)
                {
                    txtUsername.Focus();
                } else
                {
                    btnLogin_Click(sender, e);
                }
            }
        }
    }
}
