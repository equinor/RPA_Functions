using System;
using System.Data.SqlClient;   


namespace rpa_functions
{
    class CommonDb
    {
        SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
        SqlConnection sqlConn;
        static string db = Environment.GetEnvironmentVariable("DB_SERVER");
        static string db_user = Environment.GetEnvironmentVariable("DB_USER");
        static string db_passwd = Environment.GetEnvironmentVariable("DB_PASSWD");

        public CommonDb(string table)
        {
            try { 
                cb.DataSource = db;
                cb.UserID = db_user;
                cb.Password = db_passwd;
                cb.InitialCatalog = table;

                this.sqlConn = new SqlConnection(cb.ConnectionString);
                this.sqlConn.Open();


            } catch (SqlException e)
            {
                Console.WriteLine(@"SQL Exception: {0}",e);
            }
        }



    }
}
