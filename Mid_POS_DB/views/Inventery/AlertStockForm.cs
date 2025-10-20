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

namespace Mid_POS_DB.views.Inventery
{
    public partial class AlertStockForm : Form
    {
        public AlertStockForm()
        {
            InitializeComponent();
        }

        private void AlertStockForm_Load(object sender, EventArgs e)
        {
            Product product = new Product();
            product.AlertStock(dgAlterStock);
        }
    }
}
