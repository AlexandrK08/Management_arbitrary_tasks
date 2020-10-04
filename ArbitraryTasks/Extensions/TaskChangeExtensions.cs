using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;

namespace ArbitraryTasks.Extensions
{
    public static class TaskChangeExtensions
    {
        public static TaskChange_Queries GetByID(this IQueryable<TaskChange_Queries> taskChanges, UInt64 taskChangeID)
        {
            IQueryable<TaskChange_Queries> result = taskChanges.Where(t => t.ID == taskChangeID);
            return result.Count() > 0 ? result.First<TaskChange_Queries>() : null;
        }

        public static IQueryable<TaskChange_Queries> GetByTaskID(this IQueryable<TaskChange_Queries> taskChanges, UInt64 taskID)
        {
            return taskChanges.Where(c => c.TaskID == taskID);
        }

        public static Boolean Exist(this IQueryable<TaskChange_Queries> taskChanges, UInt64 taskChangeID)
        {
            return GetByID(taskChanges, taskChangeID) == null ? false : true;
        }

        public static TaskChange_Queries GetFirstByTaskID(this IQueryable<TaskChange_Queries> taskChanges, UInt64 taskID)
        {
            IQueryable<TaskChange_Queries> result = GetByTaskID(taskChanges, taskID);
            return result.Count() > 0 ? result.OrderBy(r => r.ID).First<TaskChange_Queries>() : null;
        }

        public static TaskChange_Queries GetLastByTaskID(this IQueryable<TaskChange_Queries> taskChanges, UInt64 taskID)
        {
            IQueryable<TaskChange_Queries> result = GetByTaskID(taskChanges, taskID);
            return result.Count() > 0 ? result.OrderBy(r => r.ID).Last<TaskChange_Queries>() : null;
        }        
    }
}
