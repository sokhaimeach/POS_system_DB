using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Drawing;

namespace Mid_POS_DB.models
{
    internal class Product:Role
    {
        public long Barcode { get; set; }
        public double SellPrice { get; set; }
        public int UnitInStock { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public static string PathPhoto { get; set; }

        public override void GetData(DataGridView dg)
        {
            try
            {
                _sql = "select * from View_Product_Category";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    Id = int.Parse(dr["Id"].ToString());
                    Name = dr["ProductName"].ToString();
                    Barcode = long.Parse(dr["Barcode"].ToString());
                    SellPrice = double.Parse(dr["SellPrice"].ToString());
                    UnitInStock = int.Parse(dr["UnitInStock"].ToString());
                    CategoryName = dr["CategoryName"].ToString();
                    
                    Object[] row = { Id, Name, Barcode, SellPrice, UnitInStock,  CategoryName};
                    dg.Rows.Add(row);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error Getdata role : {ex.Message}");
            }
        }

        public override void Create()
        {
            try
            {
                if (Library.IsCheckDouplicated("tblProduct", "Name", Name, 0)) return;
                if (Library.IsCheckDouplicated("tblProduct", "Barcode", Barcode, 0)) return;
                _sql = "insert into tblProduct(Name, Barcode, SellPrice, UnitInStock, Photo, CategoryId, CreateAt, CreateBy)values(@Name, @Barcode, @SellPrice, 0, @Photo, @CategoryId, GETDATE(), @CreateBy)";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", SellPrice);
                Database.cmd.Parameters.AddWithValue("@Photo", Photo);
                Database.cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Create Product successfully","Create",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error create Product : {ex.Message}");
            }
        }

        public override void DeleteById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                var click = MessageBox.Show("Do you want to delete this record?","Delete",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if( click == DialogResult.No) return;

                SqlTransaction sqlTransaction = Database.con.BeginTransaction();
                DGV = dg.SelectedRows[0];
                Id = int.Parse(DGV.Cells[0].Value.ToString());

                _sql = "delete from tblAddStock where ProductId=@ProductId";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@ProductId", Id);
                Database.cmd.ExecuteScalar();

                _sql = "delete from tblProduct where Id=@Id";
                Database.cmd = new SqlCommand( _sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@Id", Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                if (_rowEffected > 0)
                {
                    dg.Rows.Remove(this.DGV);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error Delete role : {ex.Message}");
            }
        }

        public override void Search(DataGridView dg)
        {
            try           
            {
                _sql = "select * from View_Product_Category where ProductName like '%'+@Name+'%'";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", Name);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    Id = int.Parse(dr["Id"].ToString());
                    Name = dr["ProductName"].ToString();
                    Barcode = long.Parse(dr["Barcode"].ToString());
                    SellPrice = double.Parse(dr["SellPrice"].ToString());
                    UnitInStock = int.Parse(dr["UnitInStock"].ToString());
                    CategoryName = dr["CategoryName"].ToString();

                    Object[] row = { Id, Name, Barcode, SellPrice, UnitInStock, CategoryName };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search role: {ex.Message}");
            }
        }

        public void TransferDataToControls(DataGridView dg, TextBox name, TextBox barcode, TextBox sellprice, ComboBox categoryName, PictureBox pic)
        {
            try
            {
                if(dg.Rows.Count <= 0) { return; }
                DGV = dg.SelectedRows[0];
                Id = int.Parse(DGV.Cells[0].Value.ToString());
                name.Text = DGV.Cells[1].Value.ToString();
                barcode.Text = DGV.Cells[2].Value.ToString();
                sellprice.Text = DGV.Cells[3].Value.ToString();
                categoryName.Text = DGV.Cells[5].Value.ToString();


                _sql = "select Photo from tblProduct where Id=@Id";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Id", Id);
                Database.tbl = new DataTable();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.dataAdapter.Fill(Database.tbl);
                PathPhoto = Database.tbl.Rows[0]["Photo"].ToString();
                pic.Image = Image.FromFile(PathPhoto);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error transfer data role : {ex.Message}");
            }
        }

        public override void UpdateById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                DGV = dg.SelectedRows[0];
                Id = int.Parse(DGV.Cells[0].Value.ToString());
                if (Library.IsCheckDouplicated("tblProduct", "Name", Name, Id)) return;
                if (Library.IsCheckDouplicated("tblProduct", "Barcode", Barcode, Id)) return;
                _sql = "update tblProduct set Name=@Name, Barcode=@Barcode, SellPrice=@SellPrice, Photo=@Photo, CategoryId=@CategoryId, UpdateAt=GETDATE(), UpdateBy=@UpdateBy where Id=@Id";
                Database.cmd = new SqlCommand( _sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", SellPrice);
                Database.cmd.Parameters.AddWithValue("@Photo", Photo);
                Database.cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@Id", Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Update successfully");
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error update data role : {ex.Message}");
            }
        }
    }
}
