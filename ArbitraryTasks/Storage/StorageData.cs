using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ArbitraryTasks.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace ArbitraryTasks.Storage
{
    public class StorageData : DataConnection, IStorageData
    {
        public StorageData(String configurationString)
            : base(configurationString) { }

        #region ' Roles '

        public IQueryable<Role> GetRoles { get { return GetTable<Role>(); } }

        public UInt64 InsertRole(Role role)
        {
            return Convert.ToUInt64(this.InsertWithIdentity(role));
        }

        public Boolean UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteRole(Role role)
        {
            return this.Delete(role) > 0 ? true : false;
        }

        #endregion

        #region ' Users '

        public IQueryable<User> GetUsers { get { return GetTable<User>(); } }

        public UInt64 InsertUser(User user)
        {
            return Convert.ToUInt64(this.InsertWithIdentity(user));
        }

        public Boolean UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteUser(User user)
        {
            return this.Delete(user) > 0 ? true : false;
        }

        #endregion

        #region ' Statuses '

        public IQueryable<Status> GetStatuses { get { return GetTable<Status>(); } }

        public UInt64 InsertStatus(Status status)
        {
            return Convert.ToUInt64(this.InsertWithIdentity(status));
        }

        public Boolean UpdateStatus(Status status)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteStatus(Status status)
        {
            return this.Delete(status) > 0 ? true : false;
        }

        #endregion

        #region ' Tasks '

        public IQueryable<Task> GetTasks { get { return GetTable<Task>(); } }

        public UInt64 InsertTask(Task task)
        {
            return Convert.ToUInt64(this.InsertWithIdentity(task));
        }

        public Boolean UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteTask(Task task)
        {
            return this.Delete(task) > 0 ? true : false;
        }

        #endregion

        #region ' TaskChanges '

        public IQueryable<TaskChange> GetTaskChanges
        {
            get
            {
                return GetTable<TaskChange>();
            }
        }

        public UInt64 InsertTaskChange(TaskChange taskChange)
        {
            return Convert.ToUInt64(this.InsertWithIdentity(taskChange));
        }

        public Boolean UpdateTaskChange(TaskChange taskChange)
        {
            return this.Update<TaskChange>(taskChange) > 0 ? true : false;
        }

        public Boolean DeleteTaskChange(TaskChange taskChange)
        {
            return this.Delete(taskChange) > 0 ? true : false;
        }

        #endregion
    }
}
