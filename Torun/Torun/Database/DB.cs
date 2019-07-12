using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System;
using Torun.Classes;

namespace Torun.Database
{
    public class DB
    {
        private readonly plan_tracerDBEntities db;

        public DB()
        {
            db = new plan_tracerDBEntities();
        }

        public byte DeleteTodoList(todoList todoList)
        {
            db.todoList.Remove(todoList);
            db.SaveChanges();
            return 0;
        }
        public byte EditTodoList(todoList todoList)
        {
            if (!db.todoList.Any(x => x.id == todoList.id)) return 0;
            if (db.todoList.Any(x => x.request_number == todoList.request_number && (todoList.request_number != String.Empty))) return 0;
            db.todoList.Attach(todoList);
            db.Entry(todoList).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public byte AddTodoList(todoList todoList)
        {
            if (db.todoList.Any(x => x.request_number == todoList.request_number && (todoList.request_number != String.Empty))) return 0;
            todoList.add_time = DateTime.Now;
            db.todoList.Add(todoList);
            db.SaveChanges();

            return 1;
        }
        public List<todoList> GetTodoLists(users user) {
            //return db.todoList.Where(x => x.user_id == user.id).ToList<todoList>();
            var result = from todo in db.todoList
                         where user.id == todo.user_id
                         select todo;
            return result.ToList<todoList>();
        }

        public int GetRequestCount(byte ReqType, users user)
        {
            switch (ReqType)
            {
                case 1: return db.todoList.Where(x => x.user_id == user.id).Count();
                case 2: return db.todoList.Where(x => x.status == 1 && x.user_id == user.id).Count();
                case 3: return db.todoList.Where(x => x.status == 2 && x.user_id == user.id).Count();
            }
            return 0;
        }

        public users GetUserDetail(users user)
        {
            users temp = new users();
            temp = db.users.SingleOrDefault(x => x.user_name == user.user_name);
            return temp;
        }

        public byte Login(users user)
        {
            if (db.users.Any(x => x.user_name == user.user_name))
            {
                users tempUser = db.users.FirstOrDefault(x => x.user_name == user.user_name);
                if (String.Equals(user.password, tempUser.password))
                {
                    tempUser.login_status = 1;
                    tempUser.last_login = DateTime.Now;
                    db.users.Attach(tempUser);
                    db.Entry(tempUser).State = EntityState.Modified;
                    db.SaveChanges();
                   
                    return 1; // login successfully
                }
                else return 3; // 3 : password is wrong
            }
            else return 2; // 2 : user could not find
        }

        public void LogOut(users user)
        {
            if (db.users.Any(x => x.id == user.id))
            {
                users tempUser = db.users.FirstOrDefault(x => x.id == user.id);
                tempUser.login_status = 0;
                db.users.Attach(tempUser);
                db.Entry(tempUser).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public byte Register(users user)
        {
            if (db.users.Any(x => x.user_name == user.user_name)) return 0;

            db.users.Add(user);
            db.SaveChanges();

            return 1;
        }
    }
}
