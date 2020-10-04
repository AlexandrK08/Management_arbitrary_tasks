using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArbitraryTasks;
using ArbitraryTasks.Storage;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;
using ArbitraryTasks.Extensions;
using ArbitraryTasks.Manipulations;
using Management_arbitrary_tasks.Models;
using Management_arbitrary_tasks.Utilities;

namespace Management_arbitrary_tasks.Controllers
{
    public class TaskChangeController : Controller
    {
        private IStorageData storageData;
        //private EntitiesUtility utility;

        public TaskChangeController(IStorageData storageData)
        {
            this.storageData = storageData;
            //this.utility = new EntitiesUtility(storageData);
        }

        //
        // GET: /TaskChange/
        //public ActionResult Index() { return View(); }

        [HttpGet]
        public ActionResult AddTask()
        {
            return GetViewTaskOfEvent(0, "AddTask");
        }

        [HttpPost]
        public ActionResult AddTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "AddTask");
        }

        [HttpGet]
        public ActionResult TakingTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "TakingTask");
        }

        [HttpPost]
        public ActionResult TakingTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "TakingTask");
        }

        [HttpGet]
        public ActionResult ReassignedTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "ReassignedTask");
        }

        [HttpPost]
        public ActionResult ReassignedTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "ReassignedTask");
        }

        [HttpGet]
        public ActionResult SolutionTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "SolutionTask");
        }

        [HttpPost]
        public ActionResult SolutionTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "SolutionTask");
        }

        [HttpGet]
        public ActionResult ReturnTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "ReturnTask");
        }

        [HttpPost]
        public ActionResult ReturnTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "ReturnTask");
        }

        [HttpGet]
        public ActionResult ClosingTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "ClosingTask");
        }

        [HttpPost]
        public ActionResult ClosingTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "ClosingTask");
        }

        [HttpGet]
        public ActionResult CommentingTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "CommentingTask");
        }

        [HttpPost]
        public ActionResult CommentingTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "CommentingTask");
        }

        public ActionResult DeleteTask(UInt64 taskID)
        {
            User currentUser = this.GetActiveUser(storageData);
            // Checking existence active user
            if (currentUser == null)
            {
                RedirectToAction("Enter");
            }

            return View("ResultEvent",
                new ResultEventModel
                {
                    CaptionOfEvent = "Результат удаления заявки",
                    MessageOfResult =
                    storageData.GetTasksQueries().ExistTask(taskID) ?
                    (storageData.DeleteTask(new Task { ID = taskID }) ?
                    String.Format("Пользователь «{0}» Удалил заявку ID={1}", currentUser.Name, taskID) :
                    String.Format("Заявка ID={0} не удалена!", taskID)) :
                    String.Format("В списке заявок нет заявки с ID={0}", taskID)
                });
        }

        [HttpGet]
        public ActionResult ViewTask(UInt64 taskID)
        {
            return GetViewTaskOfEvent(taskID, "ViewTask");
        }

        [HttpPost]
        public ActionResult ViewTask(TaskChangeModel response)
        {
            return ExecuteEventOfTask(response, "ViewTask");
        }


        private ActionResult GetViewTaskOfEvent(UInt64 taskID, String nameEvent)
        {
            User currentUser = this.GetActiveUser(storageData);
            // Checking existence active user
            if (currentUser == null)
            {
                RedirectToAction("Enter");
            }

            // Checking existence task
            if (nameEvent != "AddTask" && ! storageData.GetTasksQueries().ExistTask(taskID))
            {
                return View("ResultEvent",
                    new ResultEventModel
                    {
                        CaptionOfEvent = "Результат действия",
                        MessageOfResult = String.Format("Нет заявки с ID={0}", taskID)
                    });
            }

            TaskChangeModel model = new TaskChangeModel
            {
                CurrentTask = (taskID == 0) ? null : storageData.GetTasksQueries().GetByID(taskID),
                TaskChanges = (taskID == 0) ? null : storageData.GetTaskChangesQueries().GetByTaskID(taskID).ToList<TaskChange_Queries>(),
                CurrentUser = currentUser,
                Caption = String.Empty,
                Comment = String.Empty,
                NameEvent = nameEvent,
                ErrorMessages = new String[0]
            };

            return View("Task", model);
        }

        private ActionResult ExecuteEventOfTask(TaskChangeModel response, String nameEvent)
        {
            User currentUser = this.GetActiveUser(storageData);
            // Checking existence active user
            if (currentUser == null)
            {
                RedirectToAction("Enter");
            }

            CreateTaskChange createTaskChange = 
                new CreateTaskChange
                {
                    UserID = currentUser.ID,
                    Comment = response.Comment,
                    Statuses = storageData.GetStatuses,
                    TaskChanges = storageData.GetTaskChanges.Where(t => t.TaskID ==response.CurrentTaskID).ToList<TaskChange>()
                };

            // Selection of processing of event
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(String.Empty);
                }

                ResultEventModel modelResultEventModel = null;
                TaskChange newChange = null;
                switch (nameEvent)
                {
                    case "AddTask":
                        Task newTask = new Task { Caption = response.Caption };
                        TaskChange taskChange = new TaskChange { Comment = response.Comment, UserID = currentUser.ID, DateChange = DateTime.Now };
                        storageData.SaveNewTaskAndChange(newTask, taskChange);

                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Добавления заявки",
                            MessageOfResult = String.Format("Добавлена новая заявка «{0}» с ID={1}", newTask.Caption, newTask.ID)
                        };
                        //response.CurrentTask = utility.GetTaskViewByTaskID(newTask.ID);
                        break;

                    case "TakingTask":
                        createTaskChange.Taking();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Взятия заявки",
                            MessageOfResult = String.Format("Заявка ID={0} Взята пользователем «{1}»", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;

                    case "ReassignedTask":
                        createTaskChange.Reassigned();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Переназначения заявки",
                            MessageOfResult = String.Format("Заявка ID={0} была Передана пользователю «{1}»", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;

                    case "SolutionTask":
                        createTaskChange.Solution();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Решения заявки",
                            MessageOfResult = String.Format("Заявка ID={0} была Решена пользователем «{1}»", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;

                    case "ReturnTask":
                        createTaskChange.Return();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Возврата заявки",
                            MessageOfResult = String.Format("Заявка ID={0} была Возвращена пользователем «{1}»", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;

                    case "ClosingTask":
                        createTaskChange.Closing();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Закрытия заявки",
                            MessageOfResult = String.Format("Заявка ID={0} была Закрыта пользователем «{1}»", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;

                    case "CommentingTask":
                        createTaskChange.Commenting();
                        newChange = createTaskChange.NewChange;
                        modelResultEventModel = new ResultEventModel
                        {
                            CaptionOfEvent = "Результат Комментирования заявки",
                            MessageOfResult = String.Format("Пользователь «{0}» Прокомментировал заявку {1}", newChange.TaskID, currentUser.Name)
                        };
                        storageData.InsertTaskChange(newChange);
                        break;
                }

                return View("ResultEvent", modelResultEventModel); // Returning of successful result
            }
            catch (Exception e)
            {
                // Returning of error result
                return View("Task",
                    new TaskChangeModel
                    {
                        CurrentUser = currentUser,
                        CurrentTask = storageData.GetTasksQueries().GetByID(response.CurrentTaskID),
                        TaskChanges = storageData.GetTaskChangesQueries().GetByTaskID(response.CurrentTaskID).ToList<TaskChange_Queries>(),
                        Caption = response.Caption,
                        Comment = response.Comment,
                        NameEvent = nameEvent,
                        ErrorMessages = String.IsNullOrEmpty(e.Message) ? new String[0] : new String[] { e.Message }
                    });
            }
        }
    }
}
