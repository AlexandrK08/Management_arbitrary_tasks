using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ArbitraryTasks.Entities
{
    [Table(Name = "Users")]
    public class User
    {
        [PrimaryKey, Identity]
        public UInt64 ID { get; set; }

        private String name;
        [Column(Name = "Name"), NotNull]
        public String Name
        {
            get { return name; }
            set
            {
                Utilities.Validations.CheckingText(value, 35, "Не корректное имя пользователя");
                name = value;
            }
        }
    }
}
