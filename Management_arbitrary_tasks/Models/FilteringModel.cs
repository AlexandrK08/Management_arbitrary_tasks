using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArbitraryTasks.Entities;

namespace Management_arbitrary_tasks.Models
{
    public class FilteringModel
    {
        public IList<Status> AllStatuses { get; set; }

        public IList<DisplayFieldOfTask> AllFieldsOfTask { get; set; }

        public SearchingRequest SearchingRequest { get; set; }

        public String ErrorMessage { get; set; }
    }
}
