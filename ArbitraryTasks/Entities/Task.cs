using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ArbitraryTasks.Entities
{
    [Table(Name = "Tasks")]
    public class Task
    {
        [PrimaryKey, Identity]
        public UInt64 ID { get; set; }

        private String caption;
        [Column(Name = "Caption"), NotNull]
        public String Caption
        {
            get { return caption; }
            set
            {
                Utilities.Validations.CheckingText(value, 50, "Не корректный заголовок заявки");
                caption = value;
            }
        }
    }
}
