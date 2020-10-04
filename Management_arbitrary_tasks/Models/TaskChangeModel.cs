using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;
using System.ComponentModel.DataAnnotations;

namespace Management_arbitrary_tasks.Models
{
    public class TaskChangeModel
    {
        private Task_Queries _CurrentTask;
        public Task_Queries CurrentTask
        {
            get { return _CurrentTask; }
            set
            {
                _CurrentTask = value;
                CurrentTaskID = (value != null) ? value.ID : 0;
            }
        }

        public UInt64 CurrentTaskID { get; set; }

        public IList<TaskChange_Queries> TaskChanges { get; set; }

        public User CurrentUser { get; set; }

        public String Caption { get; set; }

        [Required(ErrorMessage = "Комментарий обязателен для заполнения")]
        public String Comment { get; set; }

        public String NameEvent { get; set; }

        public String[] ErrorMessages { get; set; }
    }
}
