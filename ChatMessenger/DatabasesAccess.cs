using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ChatMessenger
{
    class DatabasesAccess
    {
        public static IEnumerable<dynamic> ReturnQueryDatabase(string cmd)
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



        public static void InsertUsersDatabase(string username, string password, string role)
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
            Console.Write("\n");
        }



        public static void DeleteUsersDatabase(string username)
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
            Console.Write("\n");
        }


        public static void UpdateUserNameDatabase(string username, string newUsername)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("newUsername", newUsername);
                var affectedRows = dbcon.Execute("Update_UserName", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
            Console.Write("\n");
        }


        public static void UpdatePasswordDatabase(string username, string password)
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
            Console.Write("\n");
        }



        public static void UpdateRoleDatabase(string username, string role)
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
            Console.Write("\n");
        }



        public static void InsertMessagesDatabase(int userId, int receiverId, string message)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@senderId", userId);
                parameters.Add("@receiverId", receiverId);
                parameters.Add("@messageData", message);
                var affectedRows = dbcon.Execute("Insert_messages", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
            Console.Write("\n");
        }


        public static IEnumerable<dynamic> ReadMessagesDatabase(string cmd, int userId, int receiverId)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@senderId", userId);
                parameters.Add("@receiverId", receiverId);
                var table = dbcon.Query(cmd, parameters);
                return table;
            }
        }


        public static IEnumerable<dynamic> ReadMessagesDatabase(string cmd, int userId)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@receiverId", userId);
                var table = dbcon.Query(cmd, parameters);
                return table;
            }
        }


        public static void DeleteMessagesDatabase(int Id)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("id", Id);
                var affectedRows = dbcon.Execute("Delete_messages", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
            Console.Write("\n");
        }
    }
}
