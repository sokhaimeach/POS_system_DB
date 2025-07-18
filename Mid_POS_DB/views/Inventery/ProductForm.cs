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
        }
    }
}
