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
            sqlConnection = new SqlConnection(@"data source=TRSGBZRDP05\SQLEXPRESS;initial catalog=Torun_PlanTracerDB;persist security info=true;user id=torun_program;password=Torun.Exe;MultipleActiveResultSets=True;");
            
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
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
