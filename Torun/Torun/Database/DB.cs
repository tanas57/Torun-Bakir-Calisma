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

        public List<todoList> GetTodoLists() {
            var result = from row in db.todoList
                          join u in db.users
                          on row.user_id equals u.id
                          select new
                          {
                              row.request_number,
                              row.description,
                              u.firstname
                          };

            return null;
            //return new List<todoList>(result);

        }

        public int getRequestCount(byte ReqType)
        {
            switch (ReqType)
            {
                case 1: return db.todoList.Count(); break;
                case 2: return db.todoList.Count(); break;
            }
            return 0;
        }

        public byte Login(string username, string password)
        {

            return 0;
        }
    }
}
