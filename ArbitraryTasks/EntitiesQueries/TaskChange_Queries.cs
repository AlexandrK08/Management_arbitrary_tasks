using System;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.EntitiesQueries
{
    public class TaskChange_Queries
    {
        public UInt64 ID { get; set; }

        public UInt64 TaskID { get; set; }

        public User CreateUser { get; set; }

        public Status Status { get; set; }

        public DateTime DateChange { get; set; }

        public String Comment { get; set; }
    }
}
