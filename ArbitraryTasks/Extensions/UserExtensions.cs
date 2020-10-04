using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.Extensions
{
    public static class UserExtensions
    {
        public static User GetUser(this IQueryable<User> users, UInt64 userID)
        {
            IQueryable<User> result = users.Where(u => u.ID == userID);
            return result.Count() > 0 ? result.First<User>() : null;
        }

        public static Boolean ExistUser(this IQueryable<User> users, UInt64 userID)
        {
            return GetUser(users, userID) == null ? false : true;
        }

        public static Boolean ExistUser(this IQueryable<User> users, String userName)
        {
            return users.Where(u => u.Name.Trim().ToLower() == userName.Trim().ToLower()).Count() > 0 ? true : false;
        }
    }
}
