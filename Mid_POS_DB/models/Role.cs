using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Mid_POS_DB.models
{
    internal class Role:Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public string _sql;
        public int _rowEffected;
        public DataGridViewRow DGV = null;

        public override void GetData(DataGridView dg)
        {
            try
            {
                _sql = "select * from tblRole";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = int.Parse(dr["Id"].ToString());
                    this.Name = dr["RoleName"].ToString();
                    this.Status = bool.Parse(dr["Status"].ToString());
                    Object[] row = { this.Id, this.Name, this.Status };
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
                if (Library.IsCheckDouplicated("tblRole", "RoleName", Name, 0)) return;
                _sql = "insert into tblRole(RoleName, Status, CreateAt, CreateBy)values(@RoleName, @Status, GETDATE(), @CreateBy)";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", this.Name);
                Database.cmd.Parameters.AddWithValue("@Status", this.Status);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if (_rowEffected > 0)
                {
                    MessageBox.Show("Create Role successfully","Create",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error create role : {ex.Message}");
            }
        }

        public override void DeleteById(DataGridView dg)
        {
            try
            {
                if(dg.Rows.Count <= 0) return;
                var click = MessageBox.Show("Do you want to delete this record?","Delete",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if( click == DialogResult.No) return;

                DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());

                _sql = "delete from tblRole where Id=@Id";
                Database.cmd = new SqlCommand( _sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
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
                _sql = "select * from tblRole where RoleName like '%'+@Name+'%'";
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
                    this.Name = dr["RoleName"].ToString();
                    this.Status = bool.Parse(dr["Status"].ToString());
                    Object[] row = { this.Id, this.Name, this.Status };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search role: {ex.Message}");
            }
        }

        public void TransferDataToControl(DataGridView dg, TextBox name, RadioButton btrue, RadioButton bfalse)
        {
            try
            {
                if(dg.Rows.Count <= 0) { return; }
                DGV = dg.SelectedRows[0];
                name.Text = DGV.Cells[1].Value.ToString();
                if (bool.Parse(DGV.Cells[2].Value.ToString()))
                {
                    btrue.Checked = true;
                }
                else
                {
                    bfalse.Checked = true;
                }
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
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                if (Library.IsCheckDouplicated("tblRole", "RoleName", Name, Id)) return;
                _sql = "update tblRole set RoleName=@Name, Status=@Status, UpdateAt=GETDATE(), UpdateBy=@UpdateBy where Id=@Id";
                Database.cmd = new SqlCommand( _sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Status", this.Status);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                _rowEffected = Database.cmd.ExecuteNonQuery();
                if(_rowEffected > 0)
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
