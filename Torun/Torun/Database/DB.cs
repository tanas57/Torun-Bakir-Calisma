﻿using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System;
using Torun.Classes;
using Torun.Lang;
using System.Windows;

namespace Torun.Database
{
    public class DB
    {
        private readonly plan_tracerDBEntities db;
        public DB()
        {
            db = new plan_tracerDBEntities();
        }
        public class TodoListGrid
        {
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
            public int Priority { get; set; }
            public string PriorityString => Functions.PriorityString((PriorityType)Priority);
            public string Description { get; set; }
            public DateTime AddTime { get; set; }
            public int Status { get; set; }
            public string StatusString =>  Functions.StatusString((StatusType)Status);
        }
        public class WeeklyPlan
        {
            public int PlanID { get; set; }
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
            public string WorkDescription { get; set; }
        }
        public class WeeklyPlanDetail : WeeklyPlan
        {
            private ILanguage Lang => CurrentLanguage.Language;
            public DateTime PlanDate { get; set; }
            public string PriorityString => Functions.PriorityString((PriorityType)Priority);
            public int Priority { get; set; }
            public DateTime AddDate { get; set; }
        }
        public class WorkDoneList
        {
            public int WorkDoneID { get; set; }
            public int WorkID { get; set; }
            public string RequestNumber { get; set; }
            public DateTime WorkDoneTime { get;set; }
            public string Description { get; set; }
        }
        public class UserInfo
        {
            public int UserID { get; set; }
            public string FullName { get; set; }
        }
        public class WorkDoneandPlans : WeeklyPlanDetail
        {
            // req num priority plan date workdone date
            public DateTime WorkDoneDate { get; set; }
        }
        public string CheckListDescriptionByID(int id)
        {
            var result = db.RoutineWorks.SingleOrDefault(x => x.id == id);
            if (result != null) return result.work_description;

            return String.Empty;
        }
        public List<WeeklyPlanDetail> ReportSearchButton(User user, string query)
        {
            // search all plans and todolist
            var result = (from plans in db.Plans
                         join todolist in db.TodoLists on plans.work_id equals todolist.id
                         where todolist.request_number.Contains(query) || todolist.description.Contains(query) && todolist.user_id == user.id
                          select new WeeklyPlanDetail
                          {
                              WorkID = todolist.id,
                              PlanID = plans.id,
                              PlanDate = plans.work_plan_time,
                              Priority = todolist.priority,
                              RequestNumber = todolist.request_number,
                              AddDate = plans.add_time
                          }).ToList();
            // search workdones
            var result2 = (from plans in db.Plans
                           join todolist in db.TodoLists on plans.work_id equals todolist.id
                           join work in db.WorkDones on plans.id equals work.plan_id
                           where work.description.Contains(query) && todolist.user_id == user.id
                           select new WeeklyPlanDetail
                           {
                               WorkID = todolist.id,
                               PlanID = plans.id,
                               PlanDate = plans.work_plan_time,
                               Priority = todolist.priority,
                               RequestNumber = todolist.request_number,
                               AddDate = plans.add_time
                           }).ToList();
            
            foreach (var item in result2)
            {
                foreach (var item2 in result)
                {
                    if (GetTodoByID(item2.WorkID).id != GetTodoByID(item.WorkID).id)
                    {
                        result.Add(item);
                        break;
                    }
                }
            }

            return result;
        }
        public void UpdateCheckListRecord(User user, string ticks, DateTime today)
        {
            RoutineWorkRecord update = GetCheckListRecord(user, today);
            if(update != null)
            {
                update.work_Ticks = ticks;
                db.RoutineWorkRecords.Attach(update);
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public int AddCheckListRecord(User user, string ticks, DateTime today)
        {
            if(!(db.RoutineWorkRecords.Where(x => x.user_id == user.id && x.add_date == today).Count() > 0))
            {
                RoutineWorkRecord record = new RoutineWorkRecord()
                {
                    user_id = user.id,
                    add_date = today,
                    work_Ticks = ticks
                };
                db.RoutineWorkRecords.Add(record);
                db.SaveChanges();

                return record.id;
            }
            return 0;
        }
        public RoutineWorkRecord GetCheckListRecord(User user, DateTime today) => db.RoutineWorkRecords.FirstOrDefault(x => x.user_id == user.id && x.add_date == today);
        public int AddUserRelationShip(User user, int rel_id)
        {
            RoutineWorkRelationShip rel = new RoutineWorkRelationShip()
            {
                other_user_id = rel_id,
                user_id = user.id
            };

            db.RoutineWorkRelationShips.Add(rel);
            db.SaveChanges();

            return rel.id;
        }
        public bool RemoveRelationShip(User user, int rel_id)
        {
            var del = db.RoutineWorkRelationShips.FirstOrDefault(x => x.user_id == user.id && x.other_user_id == rel_id);
            if (del == null) return false;

            db.RoutineWorkRelationShips.Remove(del);
            db.SaveChanges();

            return true;
        }
        public bool UserRelationShipControl(User user, int rel_id)
        {
            var result = db.RoutineWorkRelationShips.Count(x => x.user_id == user.id && x.other_user_id == rel_id);

            if (result > 0) return true;
            else return false;
        }
        public User GetUsersRelationShipWithOtherUser(User user)
        {
            var rel = db.RoutineWorkRelationShips.FirstOrDefault(x => x.other_user_id == user.id);
            if(rel != null)
            {
                return db.Users.FirstOrDefault(x => x.id == rel.user_id);
            }
            return user;
        }
        public List<UserInfo> GetUsersRelationShip(User user)
        {
            var result = from users in db.Users
                         join relation in db.RoutineWorkRelationShips on users.id equals relation.other_user_id
                         where relation.user_id == user.id
                         orderby users.firstname ascending
                         select new UserInfo
                         {
                             UserID = users.id,
                             FullName = users.firstname + " " + users.lastname
                         };
            return result.ToList();
        }
        public List<UserInfo> GetUsers(User user, bool withMe = false)
        {
            if (withMe)
            {
                var result = from users in db.Users
                             orderby users.firstname ascending
                             select new UserInfo
                             {
                                 UserID = users.id,
                                 FullName = users.firstname + " " + users.lastname
                             };
                return result.ToList();
            }
            else{
                var result = from users in db.Users
                             where users.id != user.id
                             orderby users.firstname ascending
                             select new UserInfo
                             {
                                 UserID = users.id,
                                 FullName = users.firstname + " " + users.lastname
                             };
                return result.ToList();
            }
            
            
        }
        public byte DeleteRoutineWork(RoutineWork routineWork)
        {
            db.RoutineWorks.Remove(routineWork);
            db.SaveChanges();
            return 0;
        }
        public void EditRoutineWork(RoutineWork routineWork)
        {
            db.RoutineWorks.Attach(routineWork);
            db.Entry(routineWork).State = EntityState.Modified;
            db.SaveChanges();
        }
        public List<RoutineWork> GetRoutineWorks(User user)
        {
            //return db.todoList.Where(x => x.user_id == user.id).ToList<todoList>();
            var result = from works in db.RoutineWorks
                         where works.user_id == user.id
                         orderby works.id ascending
                         select works;

            return result.ToList();
        }
        public int AddRoutineWork(RoutineWork routineWork)
        {
            db.RoutineWorks.Add(routineWork);
            db.SaveChanges();

            return routineWork.id;
        }
        public int AddBackup(Backup backup)
        {
            if(backup != null)
            {
                backup.backup_datetime = DateTime.Now;
                db.Backups.Add(backup);
                db.SaveChanges();
                return backup.id;
            }

            return 0;
        }
        /// <summary>
        /// List all backups from database
        /// </summary>
        /// <param name="user">fill the current user</param>
        /// <param name="order">false : asc order, true : desc order</param>
        /// <returns></returns>
        ///
        public Backup GetLastBackup(User user)
        {
            return db.Backups.OrderBy(x => x.id).FirstOrDefault(x => x.user_id == user.id);
        }
        public List<Backup> GetBackups(User user, bool order = false)
        {
            IQueryable<Backup> result;
            if (order)
            {
                result = from backup in db.Backups
                             where backup.user_id == user.id
                             orderby backup.id descending
                             select backup;
            }
            else
            {
                result = from backup in db.Backups
                             where backup.user_id == user.id
                             orderby backup.id ascending
                             select backup;
            }

            return result.ToList();
        }
        public Backup GetBackup(User user, int backup_id)
        {
            return db.Backups.FirstOrDefault(x => x.user_id == user.id && x.id == backup_id);
        }
        public void AddBackupRange(List<Backup> backups)
        {
            db.Backups.AddRange(backups);
            db.SaveChanges();
        }
        public void AddLog(Log log)
        {
            log.log_date = DateTime.Now;
            db.Logs.Add(log);
            db.SaveChanges();
        }
        public int UpdateUser(User user)
        {
            if (!db.Users.Any(x => x.id == user.id)) return 0;
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }
        public int UpdateUserSettings(User user, Setting setting)
        {
            if (!db.Settings.Any(x => x.user_id == user.id)) return 0;
            db.Settings.Attach(setting);
            db.Entry(setting).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }
        public void AddSettings(User user)
        {
            db.Settings.Add(new Setting() { user_id = user.id});
            db.SaveChanges();
        }
        public Setting GetUserSettings(User user) => db.Settings.SingleOrDefault(x => x.user_id == user.id);
        public List<WorkDoneandPlans> GetWorkDoneAndPlansbyDate(User user,DateTime dateTime)
        {
            var plans = from plan in db.Plans
                        join work in db.TodoLists on plan.work_id equals work.id
                        where plan.work_plan_time == dateTime.Date && work.user_id == user.id
                        orderby plan.id ascending
                        select new WorkDoneandPlans
                        {
                            PlanDate = plan.work_plan_time,
                            Priority = work.priority,
                            RequestNumber = work.request_number,
                            AddDate = plan.add_time,
                            PlanID = plan.id
                        };
            List<WorkDoneandPlans>workDoneandPlans =  plans.ToList();
            for (int i = 0; i < workDoneandPlans.Count(); i++)
            {
                WorkDone workDone = GetWorkDoneByPlanID(workDoneandPlans[i].PlanID);
                if (workDone != null)
                {
                    workDoneandPlans[i].WorkDescription = workDone.description;
                    workDoneandPlans[i].WorkDoneDate = workDone.workDoneTime.Value;
                }
            }
            return workDoneandPlans;

        }
        public List<WeeklyPlan> ListWeeklyPlanbyDate(User user, DateTime dateTime)
        {
            var result = from day in db.Plans
                         join work in db.TodoLists on day.work_id equals work.id
                         where day.work_plan_time == dateTime && user.id == work.user_id
                         orderby day.add_time ascending
                         select new WeeklyPlan
                         {
                             PlanID = day.id,
                             WorkID = work.id,
                             RequestNumber = work.request_number,
                             WorkDescription = work.description
                         };
            return result.ToList();

        }
        public List<WorkDoneandPlans> GetWorkDoneAndPlansForReport(User user, CountType countType, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes = null;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime start = dateTimes[0];
            DateTime end = dateTimes[1];

            var plans = from plan in db.Plans
                        join work in db.TodoLists on plan.work_id equals work.id
                        where work.user_id == user.id
                        where plan.work_plan_time >= start && plan.work_plan_time <= end
                        orderby plan.id ascending
                        select new WorkDoneandPlans
                        {
                            PlanDate = plan.work_plan_time,
                            Priority = work.priority,
                            RequestNumber = work.request_number,
                            AddDate = plan.add_time
                        };

            var wDone = from done in db.WorkDones
                         join plan in db.Plans on done.plan_id equals plan.id
                         join work in db.TodoLists on plan.work_id equals work.id
                         where work.user_id == user.id && done.workDoneTime >= start && done.workDoneTime <= end
                         select new WorkDoneandPlans
                         {
                             PlanDate = plan.work_plan_time,
                             Priority = work.priority,
                             RequestNumber = work.request_number,
                             WorkDoneDate = done.workDoneTime.Value
                         };
            List<WorkDoneandPlans> merge = new List<WorkDoneandPlans>();
            merge.AddRange(plans.ToList());
            merge.AddRange(wDone.ToList());
            return merge;

        }
        public List<WorkDoneList> GetWorkDoneForReport(User user, CountType countType,List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes = null;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime start = dateTimes[0];
            DateTime end = dateTimes[1];
            var result = from done in db.WorkDones
                         join plan in db.Plans on done.plan_id equals plan.id
                         join work in db.TodoLists on plan.work_id equals work.id
                         where work.user_id == user.id && done.workDoneTime >= start && done.workDoneTime <= end
                         select new WorkDoneList
                         {
                             WorkDoneID = done.id,
                             WorkID = work.id,
                             RequestNumber = work.request_number,
                             WorkDoneTime = done.workDoneTime.Value,
                             Description = done.description
                         };

            return result.ToList();

        }
        public List<WeeklyPlanDetail> GetPlansForReport(User user, CountType countType, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes = null;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime start = dateTimes[0];
            DateTime end = dateTimes[1];

            if (countType == CountType.FromTheBeginning) start = user.register_date;  // report date bug fix.

            var result = from plan in db.Plans
                         join work in db.TodoLists on plan.work_id equals work.id
                         where work.user_id == user.id
                         where plan.work_plan_time >= start && plan.work_plan_time <= end
                         orderby plan.id ascending
                         select new WeeklyPlanDetail
                         {
                             WorkID = work.id,
                             PlanID = plan.id,
                             PlanDate = plan.work_plan_time,
                             Priority = work.priority,
                             RequestNumber = work.request_number,
                             AddDate = plan.add_time
                         };
            return result.ToList();
        }
        public List<WeeklyPlanDetail> GetWeeklyPlanDetail(User user, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.NameAsc:
                    var result = from plans in db.Plans
                                 join work in db.TodoLists on plans.work_id equals work.id
                                 where plans.status != 1 && work.user_id == user.id
                                 orderby work.request_number ascending
                                 select new WeeklyPlanDetail
                                 {
                                     WorkID = work.id,
                                     RequestNumber = work.request_number,
                                     PlanID = plans.id,
                                     PlanDate = plans.work_plan_time,
                                     Priority = work.priority
                                 };
                    return result.ToList();
                case OrderBy.NameDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where plans.status != 1 && work.user_id == user.id
                             orderby work.request_number descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Priority = work.priority
                             };
                    return result.ToList();
                case OrderBy.PriorityAsc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where plans.status != 1 && work.user_id == user.id
                             orderby work.priority ascending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Priority = work.priority
                             };
                    return result.ToList();
                case OrderBy.PriorityDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where plans.status != 1 && work.user_id == user.id
                             orderby work.priority descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Priority = work.priority
                             };
                    return result.ToList();
                case OrderBy.AddedTimeAsc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where plans.status != 1 && work.user_id == user.id
                             orderby plans.work_plan_time ascending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Priority = work.priority
                             };
                    return result.ToList(); 
                default:
                case OrderBy.AddedTimeDesc:
                    result = from plans in db.Plans
                             join work in db.TodoLists on plans.work_id equals work.id
                             where plans.status != 1 && work.user_id == user.id
                             orderby plans.work_plan_time descending
                             select new WeeklyPlanDetail
                             {
                                 WorkID = work.id,
                                 RequestNumber = work.request_number,
                                 PlanID = plans.id,
                                 PlanDate = plans.work_plan_time,
                                 Priority = work.priority
                             };
                    return result.ToList();
            }
        }
        public WorkDone GetWorkDoneByID(int id) => db.WorkDones.SingleOrDefault(x => x.id == id);
        public WorkDone GetWorkDoneByPlanID(int plan_id) => db.WorkDones.SingleOrDefault(x => x.plan_id == plan_id);
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
                                     RequestNumber = work.request_number,
                                     Description = works.description
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
                                     RequestNumber = work.request_number,
                                     Description = works.description
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
                                     RequestNumber = work.request_number,
                                     Description = works.description
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
                                     RequestNumber = work.request_number,
                                     Description = works.description
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
                                 RequestNumber = work.request_number,
                                 Description = works.description
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
                                     RequestNumber = work.request_number,
                                     Description = works.description
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
                                     RequestNumber = work.request_number,
                                     WorkDescription = work.description
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
                                     RequestNumber = work.request_number,
                                     WorkDescription = work.description
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
                                     RequestNumber = work.request_number,
                                     WorkDescription = work.description
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
                                     RequestNumber = work.request_number,
                                     WorkDescription = work.description
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
                                 RequestNumber = work.request_number,
                                 WorkDescription = work.description
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
                                     RequestNumber = work.request_number,
                                     WorkDescription = work.description
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
            todoList.add_time = DateTime.Now;

            db.TodoLists.Add(todoList);
            db.SaveChanges();

            return todoList.id;
        }
        public List<TodoListGrid> GetTodoLists(User user) {
            //return db.todoList.Where(x => x.user_id == user.id).ToList<todoList>();
            var result = from todo in db.TodoLists
                         where user.id == todo.user_id && (todo.status != (int)StatusType.Closed && todo.status != (int)StatusType.Planned)
                         select new TodoListGrid
                         {
                             WorkID = todo.id,
                             RequestNumber = todo.request_number,
                             Status = todo.status,
                             AddTime = todo.add_time,
                             Description = todo.description,
                             Priority = todo.priority
                         };
            return result.ToList<TodoListGrid>();
        }
        public int GetRequestCount(byte ReqType, User user, CountType countType, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime timeStart = dateTimes[0];
            DateTime timeEnd = dateTimes[1];
            switch (ReqType)
            {
                case 1: return db.TodoLists.Where(x => x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
                case 2: return db.TodoLists.Where(x => x.status != (int)StatusType.Closed && x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
                case 3: return db.TodoLists.Where(x => x.status == (int)StatusType.Closed && x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd).Count();
            }
            return 0;
        }
        public int GetRequestCount(PriorityType priorityType, User user, CountType countType, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime timeStart = dateTimes[0];
            DateTime timeEnd = dateTimes[1];
            return db.TodoLists.Where(x => x.user_id == user.id && x.add_time >= timeStart && x.add_time <= timeEnd && x.priority == (int)priorityType).Count();
        }
        public User GetUserByID(int id) => db.Users.SingleOrDefault(x => x.id == id);
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
