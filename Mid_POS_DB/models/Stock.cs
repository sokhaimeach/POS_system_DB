using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Mid_POS_DB.views.user;

namespace Mid_POS_DB.models
{
    internal class Stock:Action
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public long Quantity { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }

        private static string _sql;
        private DataGridViewRow DGV;

        public void TransferDataToControls(DataGridView dg, TextBox productid, TextBox productname)
        {
            try
            {
                if (dg.Rows.Count <= 0) { return; }
                DGV = dg.SelectedRows[0];
                productid.Text = DGV.Cells[0].Value.ToString();
                productname.Text = DGV.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error transfer data product to stock : {ex.Message}");
            }
        }

        public override void Create()
        {
            try
            {
                SqlTransaction sqlTransaction = Database.con.BeginTransaction();
                _sql = "insert into tblAddStock(SupplierId, ProductId, Qty, Cost, CreateAt, CreateBy)values(@SupplierId, @ProductId, @Qty, @Cost, GETDATE(), @CreateBy);";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                Database.cmd.Parameters.AddWithValue("@ProductId", ProductId);
                Database.cmd.Parameters.AddWithValue("@Qty", Quantity);
                Database.cmd.Parameters.AddWithValue("@Cost", Cost);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                Database.cmd.ExecuteScalar();

                _sql = "update tblProduct set UnitInStock = UnitInStock + @Qty where Id = @ProductId";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@Qty", Quantity);
                Database.cmd.Parameters.AddWithValue("@ProductId", ProductId);
                Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit();

                MessageBox.Show("Create Product successfully", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error create Product : {ex.Message}");
            }
        }
    }
}
