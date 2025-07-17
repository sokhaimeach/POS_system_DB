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
        public static bool IsCheckDouplicated(string tblName, string colName, object values,int id)
        {
            bool check = false;
            int getId = -1;
            try
            {
                string sql = $"select * from {tblName} where {colName}=@Value";
                Database.cmd = new SqlCommand(sql, Database.con);
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
