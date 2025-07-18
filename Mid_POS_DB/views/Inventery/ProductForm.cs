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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            Library.SetName("tblCategory", "Name", cboCategory);
            Product product = new Product();
            product.GetData(dgProduct);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picProduct.Image = Image.FromFile(openFileDialog.FileName);
                Product.PathPhoto = openFileDialog.FileName;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtProductName, txtBarcode, txtSellprice)) return;
            if (cboCategory.Text == string.Empty) return;
            Product product = new Product();
            product.Name = txtProductName.Text.Trim();
            product.Barcode = long.Parse(txtBarcode.Text.Trim());
            product.SellPrice = double.Parse(txtSellprice.Text.Trim());
            product.Photo = Product.PathPhoto;
            product.CategoryId = Library.GetIdByName("tblCategory", "Name", cboCategory.Text.Trim());
            product.Create();
            product.GetData(dgProduct);
            Library.ClearTextBox(txtProductName, txtBarcode, txtSellprice);
            picProduct.Image = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.DeleteById(dgProduct);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search Product")
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search Product";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                Product product = new Product();
                product.Name = txtSearch.Text.Trim();
                product.Search(dgProduct);
            }
        }

        private void dgProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Product product = new Product();
            product.TransferDataToControls(dgProduct, txtProductName, txtBarcode, txtSellprice, cboCategory, picProduct);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Library.IsEmptyTextBox(txtProductName, txtBarcode, txtSellprice)) return;
            if (cboCategory.Text == string.Empty) return;
            Product product = new Product();
            product.Name = txtProductName.Text.Trim();
            product.Barcode = long.Parse(txtBarcode.Text.Trim());
            product.SellPrice = double.Parse(txtSellprice.Text.Trim());
            product.Photo = Product.PathPhoto;
            product.CategoryId = Library.GetIdByName("tblCategory", "Name", cboCategory.Text.Trim());
            product.UpdateById(dgProduct);
            product.GetData(dgProduct);
            Library.ClearTextBox(txtProductName, txtBarcode, txtSellprice);
            picProduct.Image = null;
        }
    }
}
