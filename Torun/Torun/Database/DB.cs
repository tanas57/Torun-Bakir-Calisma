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
        public class WeeklyPlan
        {
            public int PlanID { get; set; }
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
        }
        public DB()
        {
            db = new plan_tracerDBEntities();
        }
        public List<WorkDone> GetWorkdoneByID(int work_id)
        {
            //join work in db.todoList on day.work_id equals work.id
            var result = from workdone in db.WorkDone
                         join plan in db.plans on workdone.plan_id equals plan.id
                         where plan.work_id == work_id
                         select workdone;
            return result.ToList<WorkDone>();
        }
        public void MoveWorkToWorkDone(WorkDone workdone)
        {
            db.WorkDone.Add(workdone);
            db.SaveChanges();
        }

        public void RemovePlan(plans plan)
        {
            db.plans.Remove(plan);
            db.SaveChanges();
        }
        public List<plans> PlanToCalendar(int work_id, bool withoutCompleted = false)
        {
            if (withoutCompleted == false) // all plans list
            {
                var result = from day in db.plans
                             where day.work_id == work_id
                             select day;
                return result.ToList<plans>();
            }
            else // plans without workdone
            {
                var result = from day in db.plans
                             where day.work_id == work_id && day.status != 1 // 0 : to continue this work
                             select day;
                return result.ToList<plans>();
            }
                    
        }
        public byte EditPlan(plans plan)
        {
            if (!db.plans.Any(x => x.id == plan.id)) return 0;
            db.plans.Attach(plan);
            db.Entry(plan).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public plans GetPlanByID(int id) => db.plans.SingleOrDefault(x => x.id == id);
        public todoList GetTodoByID(int id) => db.todoList.SingleOrDefault(x => x.id == id);
        public List<WeeklyPlan> ListWeeklyPlanDay(users user, DateTime dateTime)
        {
            var result = from day in db.plans
                         join work in db.todoList on day.work_id equals work.id
                         where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                         select new WeeklyPlan
                         {
                             PlanID = day.id,
                             WorkID = work.id,
                             RequestNumber = work.request_number
            };
            return result.ToList<WeeklyPlan>();
        }
        public void AddPlanDates(plans plans)
        {
            db.plans.Add(plans);
            db.SaveChanges();
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
            db.todoList.Attach(todoList);
            db.Entry(todoList).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public byte AddTodoList(todoList todoList)
        {
            if(int.TryParse(todoList.request_number, out int req_num))
            {
                if (db.todoList.Any(x => x.request_number == req_num.ToString())) return 0; // if request is integer, the request number have to identity
            }
            // otherwise any string can add to db
            todoList.add_time = DateTime.Now;
            db.todoList.Add(todoList);
            db.SaveChanges();

            return 1;
        }
        public List<todoList> GetTodoLists(users user) {
            //return db.todoList.Where(x => x.user_id == user.id).ToList<todoList>();
            var result = from todo in db.todoList
                         where user.id == todo.user_id && ( todo.status == (int)StatusType.Added || todo.status == (int)StatusType.Edited )
                         select todo;
            return result.ToList<todoList>();
        }

        public int GetRequestCount(byte ReqType, users user)
        {
            switch (ReqType)
            {
                case 1: return db.todoList.Where(x => x.user_id == user.id).Count();
                case 2: return db.todoList.Where(x => x.status != (int)StatusType.Closed && x.user_id == user.id).Count();
                case 3: return db.todoList.Where(x => x.status == (int)StatusType.Closed && x.user_id == user.id).Count();
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
