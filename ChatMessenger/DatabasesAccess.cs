using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ChatMessenger
{
    class DatabasesAccess
    {


        public static IEnumerable<User> ReturnUsersDatabase(string cmd)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var users = dbcon.Query<User>(cmd).ToList();
                return users;
            }
        }

        public static IEnumerable<Message> ReturnMessagesDatabase(string cmd)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var messages = dbcon.Query<Message>(cmd).ToList();
                return messages;
            }
        }



        public static void ProcedureDatabase(Dictionary<string, string> DBDictionary, string nameProcedure)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                foreach (var d in DBDictionary)
                {
                    parameters.Add(d.Key, d.Value); 
                }
                var affectedRows = dbcon.Execute(nameProcedure, parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
            Console.Write("\n");
        }

        public static string ProcedureDatabases(string username, string password, string nameProcedure)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@pass", password);

                parameters.Add("@user", null, size: 100, direction: ParameterDirection.Output);

                dbcon.Execute(nameProcedure, parameters, commandType: CommandType.StoredProcedure);
                
                string user = parameters.Get<string>("@user");
                return user;
            }
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
