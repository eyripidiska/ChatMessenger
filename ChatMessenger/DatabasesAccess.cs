using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ChatMessenger
{
    class DatabasesAccess
    {
        public static IEnumerable<dynamic> ConnectDatabase(string cmd)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var table = dbcon.Query(cmd);
                return table;
            }
        }



        public static void InsertUsers(string username, string password, string role)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@pass", password);
                parameters.Add("@role", role);
                var affectedRows = dbcon.Execute("Insert_Users", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }



        }



        public static void DeleteUsers(string username)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                var affectedRows = dbcon.Execute("Delete_Users", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
        }



        public static void UpdatePassword(string username, string password)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("newPassword", password);
                var affectedRows = dbcon.Execute("Update_Users_By_Password", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
                
            }
        }



        public static void UpdateRole(string username, string role)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("newRole", role);
                var affectedRows = dbcon.Execute("Update_Users_By_Role", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");

            }
        }
    }
}
