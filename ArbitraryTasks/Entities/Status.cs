using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ArbitraryTasks.Entities
{
    [Table(Name = "Statuses")]
    public class Status
    {
        [PrimaryKey, Identity]
        public UInt64 ID { get; set; }

        [Column(Name = "Value"), NotNull]
        public Byte Value { get; set; }

        private String caption;
        [Column(Name = "Caption"), NotNull]
        public String Caption
        {
            get { return caption; }
            set
            {
                Utilities.Validations.CheckingText(value, 25, "Не корректное название статуса");
                caption = value;
            }
        }

        [Column(Name = "Description"), Nullable]
        public String Description { get; set; }
    }
}
