using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Mid_POS_DB.models
{
    internal class Sale : Product
    {
        public int Qty { get; set; } = 0;
        private int SaleId { get; set; } = 0;
        public double CashRecieve { get; set; }
        public double CashReturn { get; set; }
        public double Amount()
        {
            return Qty * SellPrice;
        }

        private double CalculateTotalAmount(DataGridView dg)
        {
            double total = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                total += double.Parse(dg.Rows[i].Cells[5].Value.ToString());
            }
            return total;
        }
        public void ScanBarcode(DataGridView dg, TextBox barcode, Label totalAmount)
        {
            try
            {

                Barcode = long.Parse(barcode.Text.Trim());
                _sql = "select * from tblProduct where Barcode=@Barcode;";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Barcode", Barcode);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    UnitInStock = int.Parse(Database.tbl.Rows[0]["UnitInStock"].ToString());
                    if (UnitInStock <= 10)
                    {
                        MessageBox.Show("Product is out of stock!");
                        return;
                    }
                    foreach (DataGridViewRow dgv in dg.Rows)
                    {
                        if (dgv.Cells[1].Value.ToString() == Barcode.ToString())
                        {
                            Qty = int.Parse(dgv.Cells[3].Value.ToString()) + 1;
                            dgv.Cells[3].Value = Qty;
                            SellPrice = double.Parse(dgv.Cells[4].Value.ToString());
                            dgv.Cells[5].Value = Amount();
                            totalAmount.Text = CalculateTotalAmount(dg).ToString("0.00");
                            barcode.Clear();
                            barcode.Focus();
                            return;
                        }
                    }

                    Id = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                    Barcode = long.Parse(Database.tbl.Rows[0]["Barcode"].ToString());
                    Name = Database.tbl.Rows[0]["Name"].ToString();
                    Qty = 1;
                    SellPrice = double.Parse(Database.tbl.Rows[0]["SellPrice"].ToString());
                    object[] row = { Id, Barcode, Name, Qty, SellPrice, Amount() };
                    dg.Rows.Add(row);
                    totalAmount.Text = CalculateTotalAmount(dg).ToString("0.00");
                    barcode.Clear();
                    barcode.Focus();
                }
                else
                {
                    MessageBox.Show("Product not found!");
                    barcode.Clear();
                    barcode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Scan barcod: {ex.Message}");
            }
        }

        public void CommitSale(DataGridView dg, Label totalAmount)
        {
            try
            {
                if (dg.Rows.Count == 0) { return; }
                foreach (DataGridViewRow row in dg.Rows)
                {
                    _sql = "select * from tblProduct where UnitInStock<@qty;";
                    Database.cmd = new SqlCommand(_sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@qty", int.Parse(row.Cells[3].Value.ToString()));
                    Database.cmd.ExecuteNonQuery();
                    Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                    Database.tbl = new DataTable();
                    Database.dataAdapter.Fill(Database.tbl);
                    if (Database.tbl.Rows.Count > 0)
                    {
                        MessageBox.Show("Product is out of stock, cannot commit sale!");
                        return;
                    }
                }

                SqlTransaction transaction = Database.con.BeginTransaction();
                _sql = "insert into tblSale(SaleDate, UserId, TotalAmount) values(GETDATE(), @UserId, @TotalAmount)select SCOPE_IDENTITY();";
                Database.cmd = new SqlCommand(_sql, Database.con, transaction);
                Database.cmd.Parameters.AddWithValue("@UserId", User.UserId);
                Database.cmd.Parameters.AddWithValue("@TotalAmount", double.Parse(totalAmount.Text.Trim()));
                SaleId = Convert.ToInt32(Database.cmd.ExecuteScalar());
                foreach (DataGridViewRow row in dg.Rows)
                {
                    _sql = "insert into tblSaleDetail(SaleId, ProductId, Qty, Price) values(@SaleId, @ProductId, @Qty, @SellPrice);";
                    Database.cmd = new SqlCommand(_sql, Database.con, transaction);
                    Database.cmd.Parameters.AddWithValue("@SaleId", SaleId);
                    Database.cmd.Parameters.AddWithValue("@ProductId", int.Parse(row.Cells[0].Value.ToString()));
                    Database.cmd.Parameters.AddWithValue("@Qty", int.Parse(row.Cells[3].Value.ToString()));
                    Database.cmd.Parameters.AddWithValue("@SellPrice", double.Parse(row.Cells[4].Value.ToString()));
                    Database.cmd.ExecuteNonQuery();

                    _sql = "update tblProduct set UnitInStock = UnitInStock - @Qty where Id = @ProductId;";
                    Database.cmd = new SqlCommand(_sql, Database.con, transaction);
                    Database.cmd.Parameters.AddWithValue("@Qty", int.Parse(row.Cells[3].Value.ToString()));
                    Database.cmd.Parameters.AddWithValue("@ProductId", int.Parse(row.Cells[0].Value.ToString()));
                    Database.cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                this.PrintSaleReport(SaleId);
                MessageBox.Show("Sale committed successfully!");
                dg.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error committing sale: {ex.Message}");
            }
        }

        public void PrintSaleReport(int id)
        {
            try
            {
                this._sql = "select * from View_Report_Sale where SaleId=@SaleId";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@SaleId", id);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);

                ReportDataSource rds = new ReportDataSource("DataSet_Report_Sale", Database.tbl);
                LocalReport rpt = new LocalReport();
                rpt.ReportPath = Application.StartupPath + @"\Reports\Report_Sale.rdlc";

                // 5. Set parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("CashRecive",CashRecieve.ToString()),
                    new ReportParameter("CashReturn",CashReturn.ToString()),
                    new ReportParameter("Total", Database.tbl.Rows[0]["TotalAmount"].ToString())
                };

                rpt.SetParameters(parameters);
                rpt.DataSources.Clear();
                rpt.DataSources.Add(rds);


                PrintReport objPrint = new PrintReport();
                objPrint.Export(rpt);
                objPrint.m_currentPageIndex = 0;
                objPrint.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Report: {ex.Message}");
            }
        }
    }
}
