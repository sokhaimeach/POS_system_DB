using Mid_POS_DB.models;
using Mid_POS_DB.views.user;
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
    public partial class StockForm : Form
    {
        public StockForm()
        {
            InitializeComponent();
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            Library.SetName("tblSupplier", "Name", cboSupplierName);
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            stock.ProductId = int.Parse(txtProductId.Text.Trim());
            stock.Quantity = long.Parse(txtqty.Text.Trim());
            stock.Cost = double.Parse(txtCost.Text.Trim());
            stock.SupplierId = Library.GetIdByName("tblSupplier", "Name", cboSupplierName.Text.Trim());
            stock.TotalCost = stock.Quantity * stock.Cost;
            stock.Create();

            Product product = new Product();
            ProductForm productForm = new ProductForm();
            product.GetData(productForm.dgProduct);
        }
    }
}
