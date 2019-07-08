using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace Torun.Database
{
    public class DB
    {
        private readonly plan_tracerDBEntities db;

        public DB()
        {
            db = new plan_tracerDBEntities();
        }
        public List<Database.users> getUsers => db.users.ToList();

        public List<Database.todoList> GetTodoLists => db.todoList.ToList();

        public int getRequestCount(byte ReqType)
        {
            switch (ReqType)
            {
                case 1: return db.todoList.Count(); break;
                case 2: return db.todoList.Count(); break;
            }
            return 0;
        }
    }
}
