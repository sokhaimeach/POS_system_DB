using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_POS_DB.models
{
    internal class Supplier : Role
    {
        public string Tel { get; set; }
        public string Address { get; set; }

        public override void GetData(DataGridView dg)
        {
            try
            {
                _sql = "select * from tblSupplier";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = int.Parse(dr["Id"].ToString());
                    this.Name = dr["Name"].ToString();
                    this.Tel = dr["Tel"].ToString();
                    this.Address = dr["Address"].ToString();
                    Object[] row = { this.Id, this.Name, this.Tel, this.Address };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Getdata supplier : {ex.Message}");
            }
        }

        public override void Create()
        {
            try
            {
                if (Library.IsCheckDouplicated("tblSupplier", "Name", Name, 0)) return;
                if (Library.IsCheckDouplicated("tblSupplier", "Tel", Tel, 0)) return;
                _sql = "insert into tblSupplier(Name, Tel, Address, CreateAt, CreateBy)values(@Name, @Tel, @Address, GETDATE(), @CreateBy)";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tel);
                Database.cmd.Parameters.AddWithValue("@Address", this.Address);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Create supplier successfully", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error create supplier : {ex.Message}");
            }
        }

        public override void DeleteById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                var click = MessageBox.Show("Do you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (click == DialogResult.No) return;

                DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());

                _sql = "delete from tblSupplier where Id=@Id";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    dg.Rows.Remove(this.DGV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Delete supplier : {ex.Message}");
            }
        }

        public override void Search(DataGridView dg)
        {
            try
            {
                _sql = "select * from tblSupplier where Name like '%'+@Name+'%'";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", Name);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = int.Parse(dr["Id"].ToString());
                    this.Name = dr["Name"].ToString();
                    this.Tel = dr["Tel"].ToString();
                    this.Address = dr["Address"].ToString();
                    Object[] row = { this.Id, this.Name, this.Tel, this.Address };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search supplier: {ex.Message}");
            }
        }

        public void TransferDataToControls(DataGridView dg, TextBox name, TextBox tel, TextBox address)
        {
            try
            {
                if (dg.Rows.Count <= 0) { return; }
                DGV = dg.SelectedRows[0];
                name.Text = DGV.Cells[1].Value.ToString();
                tel.Text = DGV.Cells[2].Value.ToString();
                address.Text = DGV.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error transfer data supplier : {ex.Message}");
            }
        }

        public override void UpdateById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                if (Library.IsCheckDouplicated("tblSupplier", "Name", Name, Id)) return;
                if (Library.IsCheckDouplicated("tblSupplier", "Tel", Tel, 0)) return;
                _sql = "update tblSupplier set Name=@Name, Tel=@Tel, Address=@Address, UpdateAt=GETDATE(), UpdateBy=@UpdateBy where Id=@Id";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tel);
                Database.cmd.Parameters.AddWithValue("@Address", this.Address);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Update successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error update data supplier : {ex.Message}");
            }
        }
    }
}
