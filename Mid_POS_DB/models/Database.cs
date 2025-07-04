using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_POS_DB.models
{
    internal class Database
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GJK1KPV\SQLEXPRESS;Initial Catalog=SV23_POS_DB;User ID=sa;Password=mathematics;Encrypt=False");
        public static SqlCommand cmd = null;
        public static SqlDataAdapter dataAdapter = null;
        public static DataTable tbl = null;
        private static bool isConnected = false;
        public static void ConnectionDB()
        {
            try
            {
                if (isConnected) return;
                con.Open();
                isConnected = true;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error connection: {ex.Message}");
            }
        }
    }
}
