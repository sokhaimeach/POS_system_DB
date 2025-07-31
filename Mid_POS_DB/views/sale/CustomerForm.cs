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

namespace Mid_POS_DB.views.sale
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.GetData(dgCustomer);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtCustomername, txtTel)) return;
            Customer customer = new Customer();
            customer.Name = txtCustomername.Text.Trim();
            customer.Gender = cboGender.Text.Trim();
            customer.Tel = txtTel.Text.Trim();
            customer.Create();
            customer.GetData(dgCustomer);
            Library.ClearTextBox(txtCustomername, txtTel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.DeleteById(dgCustomer);
        }

        private void dgCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Customer customer = new Customer();
            customer.TransferDataToControl(dgCustomer, txtCustomername, cboGender, txtTel);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtCustomername, txtTel)) return;
            Customer customer = new Customer();
            customer.Name = txtCustomername.Text.Trim();
            customer.Gender = cboGender.Text.Trim();
            customer.Tel = txtTel.Text.Trim();
            customer.UpdateById(dgCustomer);
            customer.GetData(dgCustomer);
            Library.ClearTextBox(txtCustomername, txtTel);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search customer")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search customer";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Customer customer = new Customer();
                customer.Name = txtSearch.Text.Trim();
                customer.Search(dgCustomer);
            }
        }
    }
}
