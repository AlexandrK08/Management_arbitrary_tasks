using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;

namespace Management_arbitrary_tasks.Models
{
    [Serializable]
    public class SearchingRequest
    {
        public DateTime? CreatedDate_Begin { get; set; }

        public DateTime? CreatedDate_End { get; set; }

        public DateTime? UpdatingDate_Begin { get; set; }

        public DateTime? UpdatingDate_End { get; set; }

        public Byte[] Statuses { get; set; }

        public List<Byte> SelectedStatuses
        {
            get
            {
                return (Statuses != null) ? Statuses.ToList<Byte>() : null;
            }
            set
            {
                if (value != null)
                {
                    Statuses = value.ToArray<Byte>();
                }
                else
                {
                    Statuses = null;
                }
            }
        }

        public String[] OrderOfFields { get; set; }

        public Boolean[] OrderOfValues { get; set; }
    }
}
