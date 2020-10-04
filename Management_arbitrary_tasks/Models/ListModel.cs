using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;

namespace Management_arbitrary_tasks.Models
{
    public class ListModel
    {
        public User CurrentUser { get; set; }

        public IList<Task_Queries> Tasks { get; set; }

        /*
        2   - TakingTask 
        4   - ReassignedTask 
        8   - SolutionTask 
        16  - ClosingTask 
        32  - ReturnTask 
        64  - CommentingTask 
        128 - ViewTask 
        256 - DeleteTask 
        */
        public SortedList<UInt64, Int32> AvailableActions { get; set; }
    }
}
