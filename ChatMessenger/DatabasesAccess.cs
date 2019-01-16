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

        HelpMethods hm = new HelpMethods();
        public IEnumerable<User> ReturnUsersDatabase(string cmd)
        {
            IEnumerable<User> users;
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    users = dbcon.Query<User>(cmd).ToList();
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
                users = null;
            }
            return users;
        }

        public IEnumerable<Message> ReturnMessagesDatabase(string cmd)
        {
            IEnumerable<Message> messages;
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    messages = dbcon.Query<Message>(cmd).ToList();
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
                messages = null;
            }
            return messages;
        }



        public void ProcedureDatabase(Dictionary<string, string> DBDictionary, string nameProcedure)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    var parameters = new DynamicParameters();
                    foreach (var d in DBDictionary)
                    {
                        parameters.Add(d.Key, d.Value);
                    }
                    var affectedRows = dbcon.Execute(nameProcedure, parameters, commandType: CommandType.StoredProcedure);
                    //Console.WriteLine($"{affectedRows} Affected Rows");
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
            }
        }

        public string ProcedureDatabases(string username, string password, string nameProcedure)
        {
            string user;
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@username", username);
                    parameters.Add("@pass", password);

                    parameters.Add("@user", null, size: 100, direction: ParameterDirection.Output);
                    dbcon.Execute(nameProcedure, parameters, commandType: CommandType.StoredProcedure);
                    user = parameters.Get<string>("@user");
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
                user = "";
            }
            return user;
        }


        public void InsertMessagesDatabase(int userId, int receiverId, string message)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@senderId", userId);
                    parameters.Add("@receiverId", receiverId);
                    parameters.Add("@messageData", message);
                    var affectedRows = dbcon.Execute("Insert_messages", parameters, commandType: CommandType.StoredProcedure);
                    //Console.WriteLine($"{affectedRows} Affected Rows");
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
            }
        }



        public void ProcessMessagesDatabase(int Id, string nameProcedure)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            try
            {
                using (dbcon)
                {
                    dbcon.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("id", Id);
                    var affectedRows = dbcon.Execute(nameProcedure, parameters, commandType: CommandType.StoredProcedure);
                    //Console.WriteLine($"{affectedRows} Affected Rows");
                }
            }
            catch (SqlException)
            {
                hm.MessageErrorConnection();
            }
        }
    }
}
