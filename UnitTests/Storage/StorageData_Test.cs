using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Storage;
using ArbitraryTasks.Entities;

namespace UnitTests.Storage
{
    public class StorageData_Test : IStorageData
    {
        private List<Role> roles = new List<Role>();
        private List<User> users = new List<User>();
        private List<Status> statuses = new List<Status>();
        private List<Task> tasks = new List<Task>();
        private List<TaskChange> taskChanges = new List<TaskChange>();
        private UInt64 roleID, userID, statusID, taskID, taskChangeID;

        #region ' Roles '

        public IQueryable<Role> GetRoles
        {
            get { return roles.AsQueryable<Role>(); }
        }

        public UInt64 InsertRole(Role role)
        {
            role.ID = ++roleID;
            roles.Add(role);
            return roleID;
        }

        public Boolean UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteRole(Role role)
        {
            return roles.Remove(roles.Where(r => r.ID == role.ID).First());
        }

        #endregion

        #region ' Users '

        public IQueryable<User> GetUsers
        {
            get { return users.AsQueryable<User>(); }
        }

        public UInt64 InsertUser(User user)
        {
            user.ID = ++userID;
            users.Add(user);
            return userID;
        }

        public Boolean UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteUser(User user)
        {
            return users.Remove(users.Where(u => u.ID == user.ID).First());
        }

        #endregion

        #region ' Statuses '

        public IQueryable<Status> GetStatuses
        {
            get { return statuses.AsQueryable<Status>(); }
        }

        public UInt64 InsertStatus(Status status)
        {
            status.ID = ++statusID;
            statuses.Add(status);
            return statusID;
        }

        public Boolean UpdateStatus(Status status)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteStatus(Status status)
        {
            return statuses.Remove(statuses.Where(s => s.ID == status.ID).First());
        }

        #endregion

        #region ' Tasks '

        public IQueryable<Task> GetTasks
        {
            get { return tasks.AsQueryable<Task>(); }
        }

        public UInt64 InsertTask(Task task)
        {
            task.ID = ++taskID;
            tasks.Add(task);
            return taskID;
        }

        public Boolean UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteTask(Task task)
        {
            return tasks.Remove(tasks.Where(t => t.ID == task.ID).First());
        }

        #endregion

        #region ' TaskChanges '

        public IQueryable<TaskChange> GetTaskChanges
        {
            get { return taskChanges.AsQueryable<TaskChange>(); }
        }

        public UInt64 InsertTaskChange(TaskChange taskChange)
        {
            taskChange.ID = ++taskChangeID;
            taskChanges.Add(taskChange);
            return taskChangeID;
        }

        public Boolean UpdateTaskChange(TaskChange taskChange)
        {
            throw new NotImplementedException();
        }        

        public Boolean DeleteTaskChange(TaskChange taskChange)
        {
            return taskChanges.Remove(taskChanges.Where(c => c.ID == taskChange.ID).First());
        }

        #endregion
    }
}
