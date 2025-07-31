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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            Category category = new Category();
            category.GetData(dgCategory);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtCategoryname)) return;
            Category category = new Category();
            category.Name = txtCategoryname.Text.Trim();
            category.Status = rTrue.Checked ? true : false;
            category.Create();
            category.GetData(dgCategory);
            Library.ClearTextBox(txtCategoryname);
        }

        private void dgCategory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Category category = new Category();
            category.TransferDataToControls(dgCategory, txtCategoryname, rTrue, rFalse);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtCategoryname)) return;
            Category category = new Category();
            category.Name = txtCategoryname.Text.Trim();
            category.Status = rTrue.Checked ? true : false;
            category.UpdateById(dgCategory);
            category.GetData(dgCategory);
            Library.ClearTextBox(txtCategoryname);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.DeleteById(dgCategory);
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search category";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search category")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Category category = new Category();
                category.Name = txtSearch.Text.Trim();
                category.Search(dgCategory);
            }
        }
    }
}
