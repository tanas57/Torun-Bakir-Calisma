using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using Torun.Classes.FileOperations;
namespace Torun.Database
{
    public class SQLDBBackup
    {
        private SqlConnection sqlConnection;
        
        private void OpenConnection()
        {
            string [] arr = System.Configuration.ConfigurationManager.ConnectionStrings[FileNames.DB_CONNECTION_NAME].ConnectionString.ToString().Split(';');
            sqlConnection = new SqlConnection(arr[3]);
            
            sqlConnection.Open();
        }
        public void Backup(string path)
        {
            OpenConnection();
            string sql = @"BACKUP DATABASE [Torun_PlanTracerDB] TO DISK ='" + path + "'";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void Restore(string path)
        {
            OpenConnection();
            string sql = @"RESTORE DATABASE [Torun_PlanTracerDB] FROM DISK ='" + path + "'";
            string[] commands = { "USE MASTER;", "ALTER DATABASE Torun_PlanTracerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", sql, "ALTER DATABASE Torun_PlanTracerDB SET MULTI_USER;" };
            
            SqlCommand sqlCommand;

            for (int i = 0; i < commands.Length; i++)
            {
                sqlCommand = new SqlCommand(commands[i], sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }

            sqlConnection.Close();
        }
    }
}
