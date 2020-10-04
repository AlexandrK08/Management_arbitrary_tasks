using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_arbitrary_tasks.Models
{
    public class DisplayFieldOfTask
    {
        public DisplayFieldOfTask(String name)
        {
            this.Name = name;
            this.Caption = HtmlHelpers.TasksListHelpers.ConvertingNameOfFieldToCaption(null, name);
        }

        public String Name { get; set; }

        public String Caption { get; set; }
    }
}
