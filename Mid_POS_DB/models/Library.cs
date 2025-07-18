using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Mid_POS_DB.models
{
    internal class Library
    {
        private static string _sql;
        public static bool IsCheckDouplicated(string tblName, string colName, object values,int id)
        {
            bool check = false;
            int getId = -1;
            try
            {
                _sql = $"select * from {tblName} where {colName}=@Value";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Value", values);
                Database.cmd.ExecuteNonQuery();
                Database.tbl = new DataTable();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.dataAdapter.Fill(Database.tbl);
                try
                {
                    getId = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                } catch
                {
                    getId = -1;
                }
                if (getId != id)
                {
                    if (Database.tbl.Rows.Count > 0)
                    {
                        check = true;
                        MessageBox.Show($"{colName} is douplicated, try again");
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error check douplicated : {ex.Message}");
            }
            return check;
        }

        public static void SetName(string tblName, string fillName, ComboBox cboRoleName)
        {
            try
            {
                _sql = $"select {fillName} from {tblName} order by Id";
                Database.cmd = new SqlCommand(_sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.tbl = new DataTable();
                Database.dataAdapter = new SqlDataAdapter(Database.cmd);
                Database.dataAdapter.Fill(Database.tbl);
                cboRoleName.Items.Clear();
                foreach (DataRow row in Database.tbl.Rows)
                {
                    cboRoleName.Items.Add(row[$"{fillName}"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setrolename : {ex.Message}");
            }
        }

        public static int GetIdByName(string tblName, string fillName, string value)
        {
            int id = 0;
            _sql = $"select * from {tblName} where {fillName}=@Name";
            Database.cmd = new SqlCommand(_sql, Database.con);
            Database.cmd.Parameters.AddWithValue("@Name", value);
            Database.cmd.ExecuteNonQuery();
            Database.tbl = new DataTable();
            Database.dataAdapter = new SqlDataAdapter(Database.cmd);
            Database.dataAdapter.Fill(Database.tbl);
            if (Database.tbl.Rows.Count > 0)
            {
                id = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
            }
            return id;
        }

        public static bool IsEmptyTextBox(params TextBox[] box)
        {
            bool check = false;
            foreach (TextBox boxItem in box)
            {
                if(boxItem.Text == string.Empty)
                {
                    boxItem.Focus();
                    return true;
                }
            }
            return check;
        }

        public static void ClearTextBox(params TextBox[] boxes)
        {
            foreach(TextBox boxItem in boxes)
            {
                if( boxItem.Text != string.Empty)
                {
                    boxItem.Text = string.Empty;
                }
            }
        }
    }
}
