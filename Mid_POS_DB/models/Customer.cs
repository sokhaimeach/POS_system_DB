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
    internal class Customer : Supplier
    {
        public string Gender { get; set; }

        public override void GetData(DataGridView dg)
        {
            try
            {
                _sql = "select * from tblCustomer";
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
                    this.Gender = dr["Gender"].ToString();
                    this.Tel = dr["Tel"].ToString();
                    Object[] row = { this.Id, this.Name, this.Gender, this.Tel };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Getdata customer : {ex.Message}");
            }
        }

        public override void Create()
        {
            try
            {
                if (Library.IsCheckDouplicated("tblCustomer", "Name", Name, 0)) return;
                if (Library.IsCheckDouplicated("tblCustomer", "Tel", Tel, 0)) return;
                _sql = "insert into tblCustomer(Name, Gender, Tel)values(@Name, @Gender, @Tel)";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tel);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Create customer successfully", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error create customer : {ex.Message}");
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

                _sql = "delete from tblCustomer where Id=@Id";
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
                MessageBox.Show($"Error Delete customer : {ex.Message}");
            }
        }

        public override void Search(DataGridView dg)
        {
            try
            {
                _sql = "select * from tblCustomer where Name like '%'+@Name+'%'";
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
                    this.Gender = dr["Gender"].ToString();
                    this.Tel = dr["Tel"].ToString();
                    Object[] row = { this.Id, this.Name, this.Gender, this.Tel };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search customer: {ex.Message}");
            }
        }

        public void TransferDataToControl(DataGridView dg, TextBox name, ComboBox gender, TextBox tel)
        {
            try
            {
                if (dg.Rows.Count <= 0) { return; }
                DGV = dg.SelectedRows[0];
                name.Text = DGV.Cells[1].Value.ToString();
                gender.Text = DGV.Cells[2].Value.ToString();
                tel.Text = DGV.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error transfer data customer : {ex.Message}");
            }
        }

        public override void UpdateById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                if (Library.IsCheckDouplicated("tblCustomer", "Name", Name, Id)) return;
                if (Library.IsCheckDouplicated("tblCustomer", "Tel", Tel, Id)) return;
                _sql = "update tblCustomer set Name=@Name, Gender=@Gender, Tel=@Tel where Id=@Id";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tel);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Update successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error update data customer : {ex.Message}");
            }
        }
    }
}
