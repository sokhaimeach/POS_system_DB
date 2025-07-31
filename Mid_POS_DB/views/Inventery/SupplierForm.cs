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

namespace Mid_POS_DB.views.Inventery
{
    public partial class SupplierForm : Form
    {
        public SupplierForm()
        {
            InitializeComponent();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.GetData(dgSupplier);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtSuppliername, txtTel, txtAddress)) return;
            Supplier supplier = new Supplier();
            supplier.Name = txtSuppliername.Text.Trim();
            supplier.Tel = txtTel.Text.Trim();
            supplier.Address = txtAddress.Text.Trim();
            supplier.Create();
            supplier.GetData(dgSupplier);
            Library.ClearTextBox(txtSuppliername, txtTel, txtAddress);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.DeleteById(dgSupplier);
        }

        private void dgSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.TransferDataToControls(dgSupplier, txtSuppliername, txtTel, txtAddress);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtSuppliername, txtTel, txtAddress)) return;
            Supplier supplier = new Supplier();
            supplier.Name = txtSuppliername.Text.Trim();
            supplier.Tel = txtTel.Text.Trim();
            supplier.Address = txtAddress.Text.Trim();
            supplier.UpdateById(dgSupplier);
            supplier.GetData(dgSupplier);
            Library.ClearTextBox(txtSuppliername, txtTel, txtAddress);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search supplier")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search supplier";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Supplier supplier = new Supplier();
                supplier.Name = txtSearch.Text.Trim();
                supplier.Search(dgSupplier);
            }
        }
    }
}
