using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;
using ArbitraryTasks.Storage;
using ArbitraryTasks.EntitiesQueries;

namespace ArbitraryTasks.Extensions
{
    public static class IStorageDataExtensions
    {
        #region ' Statuses '

        public static UInt64 AddStatus(this IStorageData storageData, Status newStatus)
        {
            if (storageData.GetStatuses.ExistStatus(newStatus.Value))
            {
                throw new Exception("Статус с таким значением уже существует");
            }
            return storageData.InsertStatus(newStatus);
        }

        #endregion

        #region ' Users '

        public static UInt64 AddUser(this IStorageData storageData, User newUser)
        {
            if (storageData.GetUsers.ExistUser(newUser.Name))
            {
                throw new Exception("Пользователь с таким именем уже существует");
            }
            return storageData.InsertUser(newUser);
        }

        #endregion        

        #region ' Tasks '

        public static IQueryable<Task_Queries> GetTasksQueries(this IStorageData storageData)
        {
            // Grouping changes
            var changeMinAndMaxID = from h in storageData.GetTaskChanges
                                    group h.ID by h.TaskID
                                        into groupOfTask
                                    select new
                                    {
                                        TaskID = groupOfTask.Key,
                                        MinID = groupOfTask.Min(),
                                        MaxID = groupOfTask.Max()
                                    };

            // Getting list tasks
            IQueryable<Task_Queries> qTasksView =
                (from t in storageData.GetTasks
                 join c in changeMinAndMaxID on t.ID equals c.TaskID
                 join firstChange in storageData.GetTaskChanges on c.MinID equals firstChange.ID
                 join lastChange in storageData.GetTaskChanges on c.MaxID equals lastChange.ID
                 join createUser in storageData.GetUsers on firstChange.UserID equals createUser.ID
                 join lastUpdateUser in storageData.GetUsers on lastChange.UserID equals lastUpdateUser.ID
                 join status in storageData.GetStatuses on lastChange.StatusID equals status.ID
                 orderby t.ID
                 select new Task_Queries
                 {
                     ID = t.ID,
                     Caption = t.Caption,
                     CreateDate = firstChange.DateChange,
                     CreateUser = createUser,
                     LastUpdateDate = lastChange.DateChange,
                     LastUpdateUser = lastUpdateUser,
                     Status = status,
                 }).AsQueryable<Task_Queries>();

            return qTasksView;
        }

        public static void SaveNewTaskAndChange(this IStorageData storageData, Task newTask, TaskChange firstChange)
        {
            if (!storageData.GetUsers.ExistUser(firstChange.UserID))
            {
                throw new Exception(String.Format("Пользователя с ID={0} нет в списке пользователей", firstChange.UserID));
            }

            // Addition of task and his firsting change
            UInt64 idOfNewTask = storageData.InsertTask(newTask);
            newTask.ID = idOfNewTask;
            firstChange.TaskID = idOfNewTask;
            firstChange.StatusID = storageData.GetStatuses.GetStatus((Byte)0).ID;
            firstChange.ID = storageData.InsertTaskChange(firstChange);
        }

        public static Boolean DeleteTask(this IStorageData storageData, UInt64 taskID)
        {
            UInt64[] taskIDs = storageData.GetTaskChanges.Where(c => c.TaskID == taskID).Select(c => c.ID).ToArray<UInt64>();
            foreach (UInt64 cID in taskIDs)
            {
                storageData.DeleteTaskChange(new TaskChange { ID = cID });
            }
            return storageData.DeleteTask(new Task { ID = taskID });
        }

        #endregion

        #region ' TaskChanges '

        public static IQueryable<TaskChange_Queries> GetTaskChangesQueries(this IStorageData storageData)
        {
            // Getting changes
            IQueryable<TaskChange_Queries> qTaskChangesView =
                (from h in storageData.GetTaskChanges
                 join u in storageData.GetUsers on h.UserID equals u.ID
                 join s in storageData.GetStatuses on h.StatusID equals s.ID
                 orderby h.ID
                 select new TaskChange_Queries
                 {
                     ID = h.ID,
                     TaskID = h.TaskID,
                     DateChange = h.DateChange,
                     CreateUser = u,
                     Status = s,
                     Comment = h.Comment
                 }).AsQueryable<TaskChange_Queries>();

            return qTaskChangesView;
        }

        public static Boolean DeleteLastChange(this IStorageData storageData, UInt64 taskID, UInt64 userID)
        {
            IQueryable<TaskChange_Queries> taskChanges = storageData.GetTaskChangesQueries().GetByTaskID(taskID).OrderBy(c => c.ID);
            if (taskChanges.Last().CreateUser.ID == userID)
            {
                return storageData.DeleteTaskChange(new TaskChange { ID = taskChanges.Last().ID });
            }
            return false;
        }

        #endregion
    }
}
