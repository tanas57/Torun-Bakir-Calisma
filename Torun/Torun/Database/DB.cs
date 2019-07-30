using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System;
using Torun.Classes;
using Torun.Lang;

namespace Torun.Database
{
    public class DB
    {
        private readonly plan_tracerDBEntities db;
        public DB()
        {
            db = new plan_tracerDBEntities();
        }
        
        public class WeeklyPlan
        {
            public int PlanID { get; set; }
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
        }
        public class WeeklyPlanDetail : WeeklyPlan
        {
            public ILanguage Lang => CurrentLanguage.Language;
            private int priorities;
            public DateTime PlanDate { get; set; }
            public string PriorityString { get; set; }
            public int Prioritiy
            {
                get
                {
                    return priorities;
                }
                set
                {
                    priorities = (byte)value;
                    switch (value)
                    {
                        case (int)PriorityType.Low:
                            PriorityString = Lang.ComboboxPriorityLow;
                            break;
                        case (int)PriorityType.Normal:
                            PriorityString = Lang.ComboboxPriorityNormal;
                            break;
                        case (int)PriorityType.High:
                            PriorityString = Lang.ComboboxPriorityHigh;
                            break;
                        case (int)PriorityType.Urgent:
                            PriorityString = Lang.ComboboxPriorityUrgent;
                            break;
                        case (int)PriorityType.Project:
                            PriorityString = Lang.ComboboxPriorityProject;
                            break;
                    }
                }
            }
        }
        public class WorkDoneList
        {
            public int WorkDoneID { get; set; }
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
        }
        public List<WeeklyPlanDetail> GetWeeklyPlanDetail(User user, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.NameAsc:
                    var result = from plans in db.Plans
                                 join work in db.TodoLists on plans.work_id equals work.id
                                 where work.status != 1 && work.user_id == user.id
                                 orderby work.request_number ascending
                                 select new WeeklyPlanDetail
                                 {
                                     WorkID = work.id,
                                     RequestNumber = work.request_number,
                                     PlanID = plans.id,
                                     PlanDate = plans.work_plan_time,
                                     Prioritiy = work.priority
                                 };
                    return result.ToList();
                case OrderBy.NameDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where work.status != 1 && work.user_id == user.id
                             orderby work.request_number descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Prioritiy = work.priority
                             };
                    return result.ToList();
                case OrderBy.PriorityAsc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where work.status != 1 && work.user_id == user.id
                             orderby work.priority ascending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Prioritiy = work.priority
                             };
                    return result.ToList();
                case OrderBy.PriorityDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where work.status != 1 && work.user_id == user.id
                             orderby work.priority descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Prioritiy = work.priority
                             };
                    return result.ToList();
                case OrderBy.AddedTimeAsc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where work.status != 1 && work.user_id == user.id
                             orderby plans.work_plan_time ascending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Prioritiy = work.priority
                             };
                    return result.ToList(); 
                default:
                case OrderBy.AddedTimeDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where work.status != 1 && work.user_id == user.id
                             orderby plans.work_plan_time descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Prioritiy = work.priority
                             };
                    return result.ToList();
            }
        }
        public WorkDone GetWorkDoneByID(int id) => db.WorkDones.SingleOrDefault(x => x.id == id);
        public List<WorkDone> GetWorkdoneByID(int work_id)
        {
            var result = from workdone in db.WorkDones
                         join plan in db.Plans on workdone.plan_id equals plan.id
                         where plan.work_id == work_id
                         select workdone;
            return result.ToList<WorkDone>();
        }
        public void RemoveWorkdone(WorkDone workdone)
        {
            db.WorkDones.Remove(workdone);
            db.SaveChanges();
        }
        public void MoveWorkToWorkDone(WorkDone workdone)
        {
            db.WorkDones.Add(workdone);
            db.SaveChanges();
        }

        public void RemovePlan(Plan plan)
        {
            db.Plans.Remove(plan);
            db.SaveChanges();
        }
        public bool IsPlanExists(DateTime dateTime, int work_id)
        {
            var result = from plan in db.Plans
                         where plan.work_plan_time == dateTime.Date && plan.work_id == work_id
                         select plan;
            if (result.Count() == 1) return true;
            else return false;
        }
        public bool IsWorkDoneExists(DateTime dateTime, int work_id)
        {
            var result = from work in db.WorkDones
                         join plans in db.Plans on work.plan_id equals plans.id
                         join todo in db.TodoLists on plans.work_id equals todo.id
                         where todo.id == work_id && work.workDoneTime == dateTime.Date
                         select work;
            if (result.Count() == 1) return true;
            else return false;
        }
        public List<Plan> PlanToCalendar(int work_id, bool withoutCompleted = false)
        {
            if (withoutCompleted == false) // all plans list
            {
                var result = from day in db.Plans
                             where day.work_id == work_id
                             select day;
                return result.ToList<Plan>();
            }
            else // plans without workdone
            {
                var result = from day in db.Plans
                             where day.work_id == work_id && day.status != 1 // 0 : to continue this work
                             select day;
                return result.ToList<Plan>();
            }
                    
        }
        public byte EditPlan(Plan plan)
        {
            if (!db.Plans.Any(x => x.id == plan.id)) return 0;
            db.Plans.Attach(plan);
            db.Entry(plan).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public byte EditWorkDone(WorkDone workDone)
        {
            if (!db.WorkDones.Any(x => x.id == workDone.id)) return 0;
            db.WorkDones.Attach(workDone);
            db.Entry(workDone).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public Plan GetPlanByID(int id) => db.Plans.SingleOrDefault(x => x.id == id);
        public TodoList GetTodoByID(int id) => db.TodoLists.SingleOrDefault(x => x.id == id);
        public List<WorkDoneList> ListWorkDone(User user, DateTime dateTime, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.PriorityAsc:
                    var result = from works in db.WorkDones
                                 join plan in db.Plans on works.plan_id equals plan.id
                                 join work in db.TodoLists on plan.work_id equals work.id
                                 where works.workDoneTime == dateTime && user.id == work.user_id
                                 orderby work.priority ascending
                                 select new WorkDoneList
                                 {
                                     WorkDoneID = works.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();

                case OrderBy.PriorityDesc:
                    result = from works in db.WorkDones
                                 join plan in db.Plans on works.plan_id equals plan.id
                                 join work in db.TodoLists on plan.work_id equals work.id
                                 where works.workDoneTime == dateTime && user.id == work.user_id
                                 orderby work.priority descending
                                 select new WorkDoneList
                                 {
                                     WorkDoneID = works.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();
                case OrderBy.NameAsc:
                    result = from works in db.WorkDones
                                 join plan in db.Plans on works.plan_id equals plan.id
                                 join work in db.TodoLists on plan.work_id equals work.id
                                 where works.workDoneTime == dateTime && user.id == work.user_id
                                 orderby work.request_number ascending
                                 select new WorkDoneList
                                 {
                                     WorkDoneID = works.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();

                case OrderBy.NameDesc:
                    result = from works in db.WorkDones
                                 join plan in db.Plans on works.plan_id equals plan.id
                                 join work in db.TodoLists on plan.work_id equals work.id
                                 where works.workDoneTime == dateTime && user.id == work.user_id
                                 orderby work.request_number descending
                                 select new WorkDoneList
                                 {
                                     WorkDoneID = works.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();
                case OrderBy.AddedTimeAsc:
                    result = from works in db.WorkDones
                             join plan in db.Plans on works.plan_id equals plan.id
                             join work in db.TodoLists on plan.work_id equals work.id
                             where works.workDoneTime == dateTime && work.user_id == user.id
                             orderby works.add_time ascending
                             select new WorkDoneList
                             {
                                 WorkDoneID = works.id,
                                 WorkID = work.id,
                                 RequestNumber = work.request_number
                             };
                    return result.ToList();
                default:
                case OrderBy.AddedTimeDesc:
                     result = from works in db.WorkDones
                                 join plan in db.Plans on works.plan_id equals plan.id
                                 join work in db.TodoLists on plan.work_id equals work.id
                                 where works.workDoneTime == dateTime && work.user_id == user.id
                                 orderby works.add_time descending
                                 select new WorkDoneList
                                 {
                                     WorkDoneID = works.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();
            }
        }
        public List<WeeklyPlan> ListWeeklyPlanDay(User user, DateTime dateTime, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.PriorityAsc:
                 var result = from day in db.Plans
                                 join work in db.TodoLists on day.work_id equals work.id
                                 where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                                 orderby work.priority ascending
                                 select new WeeklyPlan
                                 {
                                     PlanID = day.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();
                case OrderBy.PriorityDesc:
                    result = from day in db.Plans
                                 join work in db.TodoLists on day.work_id equals work.id
                                 where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                                 orderby work.priority descending
                                 select new WeeklyPlan
                                 {
                                     PlanID = day.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();

                case OrderBy.NameAsc:
                    result = from day in db.Plans
                                 join work in db.TodoLists on day.work_id equals work.id
                                 where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                                 orderby work.request_number ascending
                                 select new WeeklyPlan
                                 {
                                     PlanID = day.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();

                case OrderBy.NameDesc:
                    result = from day in db.Plans
                                 join work in db.TodoLists on day.work_id equals work.id
                                 where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                                 orderby day.add_time descending
                                 select new WeeklyPlan
                                 {
                                     PlanID = day.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();
                case OrderBy.AddedTimeAsc:
                    result = from day in db.Plans
                             join work in db.TodoLists on day.work_id equals work.id
                             where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                             orderby day.add_time ascending
                             select new WeeklyPlan
                             {
                                 PlanID = day.id,
                                 WorkID = work.id,
                                 RequestNumber = work.request_number
                             };
                    return result.ToList();
                default:
                case OrderBy.AddedTimeDesc:
                    result = from day in db.Plans
                                 join work in db.TodoLists on day.work_id equals work.id
                                 where day.work_plan_time == dateTime && user.id == work.user_id && day.status != 1
                                 orderby day.add_time descending
                                 select new WeeklyPlan
                                 {
                                     PlanID = day.id,
                                     WorkID = work.id,
                                     RequestNumber = work.request_number
                                 };
                    return result.ToList();

            }

        }
        public int AddPlanDates(Plan plans)
        {
            db.Plans.Add(plans);
            db.SaveChanges();
            return plans.id;
        }
        public byte DeleteTodoList(TodoList todoList)
        {
            db.TodoLists.Remove(todoList);
            db.SaveChanges();
            return 0;
        }
        public byte EditTodoList(TodoList todoList)
        {
            if (!db.TodoLists.Any(x => x.id == todoList.id)) return 0;
            db.TodoLists.Attach(todoList);
            db.Entry(todoList).State = EntityState.Modified;
            db.SaveChanges();

            return 1;
        }
        public int AddTodoList(TodoList todoList)
        {
            if(int.TryParse(todoList.request_number, out int req_num))
            {
                if (db.TodoLists.Any(x => x.request_number == req_num.ToString())) return 0; // if request is integer, the request number have to identity
            }
            // otherwise any string can add to db
            todoList.add_time = DateTime.Now;
            db.TodoLists.Add(todoList);
            db.SaveChanges();

            return todoList.id;
        }
        public List<TodoList> GetTodoLists(User user) {
            //return db.todoList.Where(x => x.user_id == user.id).ToList<todoList>();
            var result = from todo in db.TodoLists
                         where user.id == todo.user_id && ( todo.status != (int)StatusType.Closed && todo.status != (int)StatusType.Planned)
                         select todo;
            return result.ToList<TodoList>();
        }

        public int GetRequestCount(byte ReqType, User user, CountType countType)
        {
            DateTime timeStart, timeEnd;
            switch (countType)
            {
                case CountType.Daily:
                    timeStart = DateTime.Now.Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Weekly:
                    timeStart = DateTime.Now.AddDays(-7).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Montly:
                    timeStart = DateTime.Now.AddMonths(-1).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Yearly:
                    timeStart = DateTime.Now.AddYears(-1).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                default:
                case CountType.FromTheBeginning:
                    timeStart = DateTime.Now.AddYears(-100).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;
            }
            switch (ReqType)
            {
                case 1: return db.TodoLists.Where(x => x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
                case 2: return db.TodoLists.Where(x => x.status != (int)StatusType.Closed && x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
                case 3: return db.TodoLists.Where(x => x.status == (int)StatusType.Closed && x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
            }
            return 0;
        }

        public User GetUserDetail(User user)
        {
            User temp = new User();
            temp = db.Users.SingleOrDefault(x => x.user_name == user.user_name);
            return temp;
        }

        public byte Login(User user)
        {
            if (db.Users.Any(x => x.user_name == user.user_name))
            {
                User tempUser = db.Users.FirstOrDefault(x => x.user_name == user.user_name);
                if (String.Equals(user.password, tempUser.password))
                {
                    tempUser.login_status = 1;
                    tempUser.last_login = DateTime.Now;
                    db.Users.Attach(tempUser);
                    db.Entry(tempUser).State = EntityState.Modified;
                    db.SaveChanges();
                   
                    return 1; // login successfully
                }
                else return 3; // 3 : password is wrong
            }
            else return 2; // 2 : user could not find
        }

        public void LogOut(User user)
        {
            if (db.Users.Any(x => x.id == user.id))
            {
                User tempUser = db.Users.FirstOrDefault(x => x.id == user.id);
                tempUser.login_status = 0;
                db.Users.Attach(tempUser);
                db.Entry(tempUser).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public byte Register(User user)
        {
            if (db.Users.Any(x => x.user_name == user.user_name)) return 0;

            db.Users.Add(user);
            db.SaveChanges();

            return 1;
        }
    }

}
