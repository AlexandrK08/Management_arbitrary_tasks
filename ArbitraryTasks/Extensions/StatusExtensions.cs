using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.Extensions
{
    public static class StatusExtensions
    {
        public static Status GetStatus(this IQueryable<Status> statuses, UInt64 statusID)
        {
            IQueryable<Status> result = statuses.Where(s => s.ID == statusID);
            return result.Count() > 0 ? result.First<Status>() : null;
        }

        public static Status GetStatus(this IQueryable<Status> statuses, Byte statusValue)
        {
            IQueryable<Status> result = statuses.Where(s => s.Value == statusValue);
            return result.Count() > 0 ? result.First<Status>() : null;
        }

        public static Boolean ExistStatus(this IQueryable<Status> statuses, UInt64 statusID)
        {
            return GetStatus(statuses, statusID) == null ? false : true;
        }

        public static Boolean ExistStatus(this IQueryable<Status> statuses, Byte statusValue)
        {
            return GetStatus(statuses, (Byte)statusValue) == null ? false : true;
        }
    }
}
