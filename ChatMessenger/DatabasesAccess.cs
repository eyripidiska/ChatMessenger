using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ChatMessenger
{
    class DatabasesAccess
    {

        string connectionString = Properties.Settings.Default.connectionString;

        public IEnumerable<User> UsersDatabase(string cmd)
        {
            IEnumerable<User> users;
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
                HelpMethods.MessageErrorConnection();
                users = null;
            }
            return users;
        }

        public IEnumerable<Message> MessagesDatabase(string cmd)
        {
            IEnumerable<Message> messages;
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
                HelpMethods.MessageErrorConnection();
                messages = null;
            }
            return messages;
        }



        public void ProcedureDatabase(Dictionary<string, string> DBDictionary, string nameProcedure)
        {
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
                HelpMethods.MessageErrorConnection();
            }
        }

        public string ProcedureDatabases(string username, string password, string nameProcedure)
        {
            string user;
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
                HelpMethods.MessageErrorConnection();
                user = "";
            }
            return user;
        }


        public void InsertMessagesDatabase(int userId, int receiverId, string message)
        {
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
                HelpMethods.MessageErrorConnection();
            }
        }



        public void ProcessMessagesDatabase(int Id, string nameProcedure)
        {
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
                HelpMethods.MessageErrorConnection();
            }
        }
    }
}
