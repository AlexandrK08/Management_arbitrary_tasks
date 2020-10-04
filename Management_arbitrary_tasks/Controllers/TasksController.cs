using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArbitraryTasks.Storage;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;
using ArbitraryTasks.Extensions;
using Management_arbitrary_tasks.Models;
using Management_arbitrary_tasks.Utilities;

namespace Management_arbitrary_tasks.Controllers
{
    public class TasksController : Controller
    {
        private IStorageData storageData;

        public TasksController(IStorageData storageData)
        {
            this.storageData = storageData;
        }

        // GET: /Tasks/
        public ActionResult Index()
        {
            return RedirectToAction("TasksList");
        }

        public ActionResult TasksList()
        {
            ListModel model = new ListModel
            {
                CurrentUser = this.GetActiveUser(storageData), // Not
                AvailableActions = new SortedList<UInt64, Int32>()
            };

            // Checking existence active user
            if (model.CurrentUser == null)
            {
                return RedirectToAction("Enter", "Entering");
            }

            // Searching
            /*
            RequestTasks requestTasks = new RequestTasks
            {
                TasksView = storageData.GetTasksView, 
                Request = this.GetFilterSettings() ?? new SearchingRequest()
            };
            */
            //requestTasks.Searching();
            //model.Tasks = requestTasks.Result;
            SearchingRequest searchingRequest = this.GetFilterSettings() ?? new SearchingRequest();
            IQueryable<Task_Queries> tasks = storageData.GetTasksQueries().Filtering(
                CreatedDate_Begin: searchingRequest.CreatedDate_Begin, CreatedDate_End: searchingRequest.CreatedDate_End,
                UpdatingDate_Begin: searchingRequest.UpdatingDate_Begin, UpdatingDate_End: searchingRequest.UpdatingDate_End, 
                valuesOfStatuses: searchingRequest.Statuses);
            tasks = tasks.Sorting(searchingRequest.OrderOfFields, searchingRequest.OrderOfValues);
            model.Tasks = tasks.ToList<Task_Queries>();

            // Formatting available actions AND Roles of Users for Development
            Int32 currentAvailable;
            foreach (Task_Queries cTask in model.Tasks)
            {
                currentAvailable = 0;

                if (cTask.Status.Value == 0 || cTask.Status.Value == 3)
                {
                    currentAvailable += 2; // TakingTask 
                }
                if (cTask.Status.Value == 1 && cTask.LastUpdateUser.ID != model.CurrentUser.ID)
                {
                    currentAvailable += 4; // ReassignedTask
                }
                if (cTask.Status.Value == 1 && cTask.LastUpdateUser.ID == model.CurrentUser.ID)
                {
                    currentAvailable += 8; // SolutionTask
                }
                if (cTask.Status.Value == 2 && cTask.CreateUser.ID == model.CurrentUser.ID)
                {
                    currentAvailable += 16; // ClosingTask
                    currentAvailable += 32; // ReturnTask
                }
                if (cTask.Status.Value != 4 &&
                    (cTask.CreateUser.ID == model.CurrentUser.ID || cTask.LastUpdateUser.ID == model.CurrentUser.ID))
                {
                    currentAvailable += 64; // CommentingTask
                }
                currentAvailable += 128; // ViewTask
                currentAvailable += 256; // DeleteTask

                model.AvailableActions.Add(cTask.ID, currentAvailable);
            }

            return View(model);
        }
    }
}
