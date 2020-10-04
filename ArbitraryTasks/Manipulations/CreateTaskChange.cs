using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArbitraryTasks.Entities;

namespace ArbitraryTasks.Manipulations
{
    public class CreateTaskChange 
    {
        private IList<TaskChange> taskChanges;
        public IList<TaskChange> TaskChanges
        {
            get
            { return taskChanges; }
            set
            {
                //if ((value.Where(c => c.ID <= 0).Count() > 0) || (value.GroupBy(c => c.ID).Count() != value.Count())) { throw new Exception("Среди предоставленных изменений имеются изменения с некорректными ID"); }
                taskChanges = value;
            }
        }

        private IQueryable<Status> statuses;
        public IQueryable<Status> Statuses
        {
            get { return statuses; }
            set
            {
                CheckingStatuses(value);
                statuses = value;
            }
        }

        public UInt64 UserID { get; set; }

        public String Comment { get; set; }

        public DateTime DateChange { get; set; }

        private TaskChange newChange;
        public TaskChange NewChange
        {
            get { return newChange; }
        }

        private void CheckingStatuses(IQueryable<Status> statuses)
        {
            Byte[] allStatuses = statuses.Select(s => s.Value).ToArray<Byte>();
            foreach (Byte cStatus in new Byte[] { 0, 1, 2, 3, 4 })
            {
                if (!allStatuses.Contains(cStatus))
                {
                    throw new Exception("Среди предоставленных статусов нет требуемого статуса");
                }
            }
        }

        private TaskChange FirstChange
        {
            get
            {
                return taskChanges.OrderBy(c => c.ID).ToList<TaskChange>().First();
            }
        }

        private TaskChange LastChange
        {
            get
            {
                return taskChanges.OrderBy(c => c.ID).ToList<TaskChange>().Last();
            }
        }

        private Byte GetStatusValueByID(UInt64 statusID)
        {
            return Statuses.Where(s => s.ID == statusID).First().Value;
        }

        private TaskChange GetLastTaking
        {
            get
            {
                TaskChange lastTaking = null;
                foreach (TaskChange cChange in taskChanges.OrderByDescending(c => c.ID))
                {
                    if (GetStatusValueByID(cChange.StatusID) != 1)
                        break;
                    lastTaking = cChange;
                }
                return lastTaking;
            }
        }

        private void CreateChange(Byte statusValue)
        {
            newChange = new TaskChange
            {
                TaskID = LastChange.TaskID,
                UserID = UserID,
                StatusID = Statuses.Where(s => s.Value == statusValue).First().ID,
                DateChange = (DateChange.Ticks == 0) ? DateTime.Now : DateChange,
                Comment = Comment
            };
        }

        public void Taking()
        {
            if (!(new Byte[] { 0, 3 }.Contains(GetStatusValueByID(LastChange.StatusID))))
            {
                throw new Exception("Взять можно только заявку со статусом «Открыта» или «Возврат»");
            }

            CreateChange(1);
        }

        public void Reassigned()
        {
            if (GetStatusValueByID(LastChange.StatusID) != 1)
            {
                throw new Exception("Переназначить можно только заявку со статусом «Взята»");
            }

            CreateChange(1);
        }

        public void Solution()
        {
            if (GetStatusValueByID(LastChange.StatusID) != 1)
            {
                throw new Exception("Решить можно только заявку со статусом «Взята»");
            }
            if ((FirstChange.UserID != UserID) && (GetLastTaking.UserID != UserID))
            {
                throw new Exception("Решить заявку может только пользователь, взявший её последним или её Автор");
            }

            CreateChange(2);
        }

        public void Return()
        {
            if (GetStatusValueByID(LastChange.StatusID) != 2)
            {
                throw new Exception("Вернуть можно только заявку со статусом «Решена»");
            }
            if (FirstChange.UserID != UserID)
            {
                throw new Exception("Вернуть заявку может только пользователь создавший её");
            }

            CreateChange(3);
        }

        public void Closing()
        {
            if (GetStatusValueByID(LastChange.StatusID) != 2)
            {
                throw new Exception("Закрыть можно только заявку со статусом «Решена»");
            }
            if (FirstChange.UserID != UserID)
            {
                throw new Exception("Закрыть заявку может только пользователь создавший её");
            }

            CreateChange(4);
        }

        public void Commenting()
        {
            if (GetStatusValueByID(LastChange.StatusID) != 1)
            {
                throw new Exception("Комментировать можно только заявку со статусом «Взята»");
            }
            if ((FirstChange.UserID != UserID) && (GetLastTaking.UserID != UserID))
            {
                throw new Exception("Комментировать заявку могут только пользователи создатель и исполнитель");
            }

            CreateChange(1);
        }
    }
}
