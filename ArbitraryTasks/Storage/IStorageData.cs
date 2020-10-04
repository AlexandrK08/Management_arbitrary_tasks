using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.Storage
{
    public interface IStorageData
    {
        #region ' Roles '

        IQueryable<Role> GetRoles { get; }

        UInt64 InsertRole(Role role);

        Boolean UpdateRole(Role role);

        Boolean DeleteRole(Role role);

        #endregion

        #region ' Users '

        IQueryable<User> GetUsers { get; }

        UInt64 InsertUser(User user);

        Boolean UpdateUser(User user);

        Boolean DeleteUser(User user);

        #endregion

        #region ' Statuses '

        IQueryable<Status> GetStatuses { get; }

        UInt64 InsertStatus(Status status);

        Boolean UpdateStatus(Status status);

        Boolean DeleteStatus(Status status);

        #endregion

        #region ' Tasks '

        IQueryable<Task> GetTasks { get; }

        UInt64 InsertTask(Task task);

        Boolean UpdateTask(Task task);

        Boolean DeleteTask(Task task);

        #endregion

        #region ' TaskChanges '

        IQueryable<TaskChange> GetTaskChanges { get; }

        UInt64 InsertTaskChange(TaskChange taskChange);

        Boolean UpdateTaskChange(TaskChange taskChange);

        Boolean DeleteTaskChange(TaskChange taskChange);

        #endregion
    }
}
