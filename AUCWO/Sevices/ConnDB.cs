using OfficeOpenXml.Packaging.Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AUCWO
{
    public class DB
    {
        //private static string connectionString = "Server=pc-tql;Database=DB_SL;User Id=sa;Password=P@ssw0rd2025!;";
        //private static string connectionString = "Server=10.239.1.162;Database=DB_SL;User Id=loi_tq;Password=249533;";
        private static string connectionString = AppData.Instance.DB;
        //private static string connectionString = "Server=10.239.1.54;Database=DB_SL;User Id=sa;Password=123456;";

        //private static string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Integrated Security=True";

        // Hàm mở kết nối
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return null;
            }
           
        }
        // Hàm kiểm tra kết nối
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true; // kết nối thành công
                }
            }
            catch
            {
                return false; // kết nối thất bại
            }
        }


        // Hàm thực thi câu lệnh INSERT, UPDATE, DELETE
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // Hàm thực thi SELECT trả về DataTable
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
