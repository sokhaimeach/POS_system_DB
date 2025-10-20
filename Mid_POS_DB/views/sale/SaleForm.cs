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
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            if (Library.IsEmptyTextBox(txtCashRecive)) return;
            double cashReceived, totalAmount, cashReturn = 0;
            totalAmount = double.Parse(lblTotalAmount.Text.Trim());
            cashReceived = double.Parse(txtCashRecive.Text.Trim());
            if (cashReceived >= totalAmount)
            {
                cashReturn = cashReceived - totalAmount;
                lblCashReturn.Text = cashReturn.ToString("0.00");
            }
            else
            {
                MessageBox.Show("Cash received is less than total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCashRecive.Clear();
                txtCashRecive.Focus();
                return;
            }
            sale.CashRecieve = cashReceived;
            sale.CashReturn = cashReturn;
            sale.CommitSale(dgSale, lblTotalAmount);
            txtScanBarcode.Clear();
            txtScanBarcode.Focus();
            txtCashRecive.Clear();
            lblCashReturn.Text = "0.00";
            lblTotalAmount.Text = "0.00";
        }

        private void txtCashRecive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Library.IsEmptyTextBox(txtCashRecive)) return;
                double cashReceived, totalAmount, cashReturn = 0;
                totalAmount = double.Parse(lblTotalAmount.Text.Trim());
                cashReceived = double.Parse(txtCashRecive.Text.Trim());
                if (cashReceived >= totalAmount)
                {
                    cashReturn = cashReceived - totalAmount;
                    lblCashReturn.Text = cashReturn.ToString("0.00");
                }
                else
                {
                    MessageBox.Show("Cash received is less than total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCashRecive.Clear();
                    txtCashRecive.Focus();
                    return;
                }
            }
        }

        private void txtScanBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Sale sale = new Sale();
                sale.ScanBarcode(dgSale, txtScanBarcode, lblTotalAmount);
            }
        }

        private void txtScanBarcode_Enter(object sender, EventArgs e)
        {
            if (txtScanBarcode.Text == "Scan barcode")
            {
                txtScanBarcode.Text = string.Empty;
                txtScanBarcode.ForeColor = Color.Black;
            }
        }

        private void txtScanBarcode_Leave(object sender, EventArgs e)
        {
            if (txtScanBarcode.Text == string.Empty)
            {
                txtScanBarcode.Text = "Scan barcode";
                txtScanBarcode.ForeColor = Color.DarkGray;
            }
        }
    }
}
