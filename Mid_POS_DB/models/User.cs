using Mid_POS_DB.views.user;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Mid_POS_DB.models
{
    internal class User : Role
    {
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public static int UserId { get; set; } = 0;
        public int RoleId { get; set; }
        private string RoleName { get; set; }
        public void Login(Form form)
        {
            try
            {
                Database.ConnectionDB();
                this._sql = "select * from tblUser where UserName=@Name and Password=@Password";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.ExecuteNonQuery();
                Database.tbl = new DataTable();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.dataAdapter.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    User.UserId = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                    Dashboard dashboard = new Dashboard();
                    dashboard.toolStripStatus.Text = Database.tbl.Rows[0]["UserName"].ToString();
                    dashboard.labelStatus.Text = dashboard.toolStripStatus.Text;
                    dashboard.Show();
                    form.Hide();
                }
                else
                {
                    MessageBox.Show("Password or Usernam is invalid, please try again", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error login : {ex.Message}");
            }
        }

        public void SetRoleName(ComboBox cboRoleName)
        {
            try
            {
                _sql = "select RoleName from tblRole";
                Database.cmd = new SqlCommand( _sql, Database.con );
                Database.cmd.ExecuteNonQuery();
                Database.tbl = new DataTable();
                Database.dataAdapter = new SqlDataAdapter( Database.cmd );
                Database.dataAdapter.Fill(Database.tbl);
                cboRoleName.Items.Clear();
                foreach (DataRow row in Database.tbl.Rows)
                {
                    cboRoleName.Items.Add(row["RoleName"].ToString());
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error setrolename : {ex.Message}");
            }
        }

        public int GetRoleId(string roleName)
        {
            int id = 0;
            _sql = "select * from tblRole where RoleName=@Name";
            Database.cmd = new SqlCommand(_sql, Database.con );
            Database.cmd.Parameters.AddWithValue("@Name", roleName);
            Database.cmd.ExecuteNonQuery();
            Database.tbl = new DataTable();
            Database.dataAdapter = new SqlDataAdapter(Database.cmd );
            Database.dataAdapter.Fill(Database.tbl);
            if(Database.tbl.Rows.Count > 0)
            {
                id = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
            }
            return id;
        }

        public override void GetData(DataGridView dg)
        {
            try
            {
                _sql = "select * from View_User_Role order by Id";
                Database.cmd = new SqlCommand( _sql, Database.con );
                Database.cmd.ExecuteNonQuery();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.dataAdapter.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {
                    Id = int.Parse(dr["Id"].ToString());
                    Name = dr["UserName"].ToString();
                    Gender = dr["Gender"].ToString();
                    Email = dr["Email"].ToString();
                    Status = bool.Parse(dr["Status"].ToString());
                    RoleName = dr["RoleName"].ToString();

                    Object[] row = {Id, Name, Gender, Email, Status, RoleName};
                    dg.Rows.Add(row);
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error getData user: {ex.Message}");
            }
        }

        public override void Create()
        {
            try
            {

                SqlTransaction sqlTransaction = null;
                sqlTransaction = Database.con.BeginTransaction();
                _sql = "insert into tblUser(UserName, Gender, Password, Email, Status, CreateBy, CreateAt)values(@UserName, @Gender, @Password, @Email, @Status, @CreateBy, GETDATE())select SCOPE_IDENTITY()";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@UserName", Name);
                Database.cmd.Parameters.AddWithValue("@Gender", Gender);
                Database.cmd.Parameters.AddWithValue("@Password", Password);
                Database.cmd.Parameters.AddWithValue("@Email", Email);
                Database.cmd.Parameters.AddWithValue("@Status", Status);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                Id = Convert.ToInt32(Database.cmd.ExecuteScalar());

                _sql = "insert into tblUserRole(UserId, RoleId, CreateBy, CreateAt)values(@UserId, @RoleId, @CreateBy, GETDATE())";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@UserId", Id);
                Database.cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                MessageBox.Show($"Create user successfully");


            } catch (Exception ex)
            {
                MessageBox.Show($"Error create user : {ex.Message}");
            }
        }

        public override void DeleteById(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0) return;
                DGV = dg.SelectedRows[0];
                Id = int.Parse(DGV.Cells[0].Value.ToString());
                SqlTransaction sqlTransaction = Database.con.BeginTransaction();
                var click = MessageBox.Show("Do you want to delete this recod?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (click == DialogResult.No) return;

                _sql = "delete from tblUserRole where UserId=@UserId";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@UserId", Id);
                Database.cmd.ExecuteScalar();

                _sql = "delete from tblUser where Id=@Id";
                Database.cmd = new SqlCommand(_sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@Id", Id);
                Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                dg.Rows.Remove(DGV);
            } catch(Exception ex)
            {
                MessageBox.Show($"Error Delete user : {ex.Message}");
            }
        }

        public override void Search(DataGridView dg)
        {
            try
            {
                _sql = "select * from View_User_Role where UserName like '%'+@Name+'%'";
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
                    Name = dr["UserName"].ToString();
                    Gender = dr["Gender"].ToString();
                    Email = dr["Email"].ToString();
                    Status = bool.Parse(dr["Status"].ToString());
                    RoleName = dr["RoleName"].ToString();

                    Object[] row = { Id, Name, Gender, Email, Status, RoleName };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search user: {ex.Message}");
            }
        }

        public void TransferDataToControl(DataGridView dg, TextBox name, ComboBox gender, TextBox pass, TextBox email, RadioButton rtrue, RadioButton rfalse, ComboBox rolaName)
        {
            try
            {

            } catch (Exception ex)
            {
                MessageBox.Show($"Error transfer data user: {ex.Message}");
            }
        }
    }
}
