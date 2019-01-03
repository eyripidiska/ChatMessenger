﻿using System;
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


        public static void InsertUsers(string username, string password)
        {
            var connectionString = Properties.Settings.Default.connectionString;
            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@pass", password);
                var affectedRows = dbcon.Execute("Insert_Users", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
        }
    }
}