using System;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.EntitiesQueries
{
    public class Task_Queries
    {
        public UInt64 ID { get; set; }

        public String Caption { get; set; }

        public DateTime CreateDate { get; set; }

        public User CreateUser { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public User LastUpdateUser { get; set; }

        public Status Status { get; set; }
    }
}
