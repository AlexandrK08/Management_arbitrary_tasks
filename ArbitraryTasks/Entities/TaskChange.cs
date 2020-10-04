using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ArbitraryTasks.Entities
{
    [Table(Name = "HistoryChangesOfTasks")]
    public class TaskChange
    {
        [PrimaryKey, Identity]
        public UInt64 ID { get; set; }

        [Column(Name = "TaskID"), NotNull]
        public UInt64 TaskID { get; set; }

        [Column(Name = "UserID"), NotNull]
        public UInt64 UserID { get; set; }

        [Column(Name = "StatusID"), NotNull]
        public UInt64 StatusID { get; set; }

        [Column(Name = "CreatedDate"), NotNull]
        public DateTime DateChange { get; set; }

        private String comment;
        [Column(Name = "Comment"), NotNull]
        public String Comment
        {
            get { return comment; }
            set
            {
                Utilities.Validations.CheckingText(value, 256, "Не корректный комментарий");
                comment = value;
            }
        }
    }
}
