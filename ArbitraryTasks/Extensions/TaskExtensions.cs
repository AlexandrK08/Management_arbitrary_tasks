using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;

namespace ArbitraryTasks.Extensions
{
    public static class TaskExtensions
    {
        #region ' Filtering '

        public static IQueryable<Task_Queries> Filtering(
            this IQueryable<Task_Queries> tasksView,
            UInt64[] IDsTasks = null,
            DateTime? CreatedDate_Begin = null, DateTime? CreatedDate_End = null,
            DateTime? UpdatingDate_Begin = null, DateTime? UpdatingDate_End = null,
            UInt64[] IDsUsersOfCreators = null, Byte[] valuesOfStatuses = null)
        {
            tasksView = GetByIDsTasks(tasksView, IDsTasks);

            tasksView = GetByCreatedDate(tasksView, CreatedDate_Begin, CreatedDate_End);

            tasksView = GetByUpdatingDate(tasksView, UpdatingDate_Begin, UpdatingDate_End);

            tasksView = GetByUsersCreators(tasksView, IDsUsersOfCreators);

            tasksView = GetByStatuses(tasksView, valuesOfStatuses);

            return tasksView;
        }

        public static IQueryable<Task_Queries> GetByIDsTasks(this IQueryable<Task_Queries> tasksView, UInt64[] tasksIDs)
        {
            if (tasksIDs != null && tasksIDs.Length > 0)
            {
                tasksView = tasksView.Where(t => (tasksIDs.Contains(t.ID)));
            }
            return tasksView;
        }

        public static IQueryable<Task_Queries> GetByCreatedDate(this IQueryable<Task_Queries> tasksView, DateTime? begin, DateTime? end)
        {
            Utilities.Validations.CheckingDates(begin, end, "Дата начала диапазона периода создания не может быть больше даты конца диапазона периода создания");

            if (begin != null)
            {
                tasksView = tasksView.Where(t => (t.CreateDate >= begin.Value.Date));
            }
            if (end != null)
            {
                tasksView = tasksView.Where(t => (t.CreateDate <= end.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
            }

            return tasksView;
        }

        public static IQueryable<Task_Queries> GetByUpdatingDate(this IQueryable<Task_Queries> tasksView, DateTime? begin, DateTime? end)
        {
            Utilities.Validations.CheckingDates(begin, end, "Дата начала диапазона периода последнего обновления не может быть больше даты конца диапазона периода последнего обновления");

            if (begin != null)
            {
                tasksView = tasksView.Where(t => (t.LastUpdateDate >= begin.Value.Date));
            }
            if (end != null)
            {
                tasksView = tasksView.Where(t => (t.LastUpdateDate <= end.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
            }

            return tasksView;
        }

        public static IQueryable<Task_Queries> GetByUsersCreators(this IQueryable<Task_Queries> tasksView, UInt64[] usersIDs)
        {
            if (usersIDs != null && usersIDs.Length > 0)
            {
                tasksView = tasksView.Where(t => (usersIDs.Contains(t.CreateUser.ID)));
            }
            return tasksView;
        }

        public static IQueryable<Task_Queries> GetByStatuses(this IQueryable<Task_Queries> tasksView, Byte[] statusesValues)
        {
            if (statusesValues != null && statusesValues.Length > 0)
            {
                tasksView = tasksView.Where(t => (statusesValues.Contains(t.Status.Value)));
            }
            return tasksView;
        }

        public static Task_Queries GetByID(this IQueryable<Task_Queries> tasksView, UInt64 taskID)
        {
            IQueryable<Task_Queries> result = tasksView.Where(t => t.ID == taskID);
            return result.Count() > 0 ? result.First<Task_Queries>() : null;
        }

        #endregion

        public static IQueryable<Task_Queries> Sorting(this IQueryable<Task_Queries> tasksView, String[] fields, Boolean[] descending)
        {
            Int32 fieldIndex = 0;
            if (fields == null)
            {
                return tasksView;
            }
            descending = descending ?? new Boolean[0];
            IOrderedQueryable<Task_Queries> ordered = null; // tasksView.OrderBy(t => t.ID);
            foreach (String cField in fields)
            {
                switch (cField.ToLower())
                {
                    case "id":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.ID);
                            else
                                ordered = tasksView.OrderBy(t => t.ID);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.ID);
                            else
                                ordered = ordered.ThenBy(t => t.ID);
                        }
                        break;
                    case "caption":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.Caption);
                            else
                                ordered = tasksView.OrderBy(t => t.Caption);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.Caption);
                            else
                                ordered = ordered.ThenBy(t => t.Caption);
                        }
                        break;
                    case "createdate":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.CreateDate);
                            else
                                ordered = tasksView.OrderBy(t => t.CreateDate);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.CreateDate);
                            else
                                ordered = ordered.ThenBy(t => t.CreateDate);
                        }
                        break;
                    case "createuser":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.CreateUser.Name);
                            else
                                ordered = tasksView.OrderBy(t => t.CreateUser.Name);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.CreateUser.Name);
                            else
                                ordered = ordered.ThenBy(t => t.CreateUser.Name);
                        }
                        break;
                    case "lastupdatedate":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.LastUpdateDate);
                            else
                                ordered = tasksView.OrderBy(t => t.LastUpdateDate);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.LastUpdateDate);
                            else
                                ordered = ordered.ThenBy(t => t.LastUpdateDate);
                        }
                        break;
                    case "lastupdateuser":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.LastUpdateUser.Name);
                            else
                                ordered = tasksView.OrderBy(t => t.LastUpdateUser.Name);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.LastUpdateUser.Name);
                            else
                                ordered = ordered.ThenBy(t => t.LastUpdateUser.Name);
                        }
                        break;
                    case "status":
                        if (fieldIndex == 0)
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = tasksView.OrderByDescending(t => t.Status.Value);
                            else
                                ordered = tasksView.OrderBy(t => t.Status.Value);
                        }
                        else
                        {
                            if (descending.ElementAtOrDefault(fieldIndex++))
                                ordered = ordered.ThenByDescending(t => t.Status.Value);
                            else
                                ordered = ordered.ThenBy(t => t.Status.Value);
                        }
                        break;
                    default:
                        throw new Exception($"No field '{cField.ToLower()}'");
                }
            }
            return (IQueryable<Task_Queries>) ordered.AsQueryable();
        }

        public static Boolean ExistTask(this IQueryable<Task_Queries> tasksView, UInt64 taskID)
        {
            return GetByID(tasksView, taskID) == null ? false : true;
        }

        //
    }
}
