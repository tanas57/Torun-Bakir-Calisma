using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            sqlConnection = new SqlConnection(@"data source=TRSGBZRDP05\SQLEXPRESS;initial catalog=Torun_PlanTracerDB;persist security info=True;user id=torun_program;password=Torun.Exe;MultipleActiveResultSets=True;");
            sqlConnection.Open();
        }
        public void Backup(string path)
        {
            OpenConnection();
            string sql = @"BACKUP DATABASE [Torun_PlanTracerDB] TO DISK ='" + path + "' WITH NOFORMAT, NOINIT,  NAME = N'data-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
