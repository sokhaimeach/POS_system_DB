using Mid_POS_DB.views.user;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_POS_DB.models
{
    internal class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public static int UserId { get; set; } = 0;
        public string _sql;
        public void Login(Form form)
        {
            Database.ConnectionDB();
            try
            {
                this._sql = "select * from tblUser where UserName=@Name and Password=@Password";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.ExecuteNonQuery();
                Database.tbl = new System.Data.DataTable();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.dataAdapter.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    User.UserId = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                    Dashboard dashboard = new Dashboard();
                    dashboard.toolStripStatus.Text = Database.tbl.Rows[0]["UserName"].ToString();
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
    }
}
