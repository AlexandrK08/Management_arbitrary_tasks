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
    public class FilteringController : Controller
    {
        private IStorageData storageData;

        public FilteringController(IStorageData storageData)
        {
            this.storageData = storageData;
        }

        //
        // GET: /Filtering/

        public ActionResult Index()
        {
            return RedirectToAction("SettingsOfFiltering");
        }

        [HttpGet]
        public ActionResult SettingsOfFiltering()
        {
            FilteringModel model = new FilteringModel
            {
                AllStatuses = storageData.GetStatuses.ToList<Status>(),

                SearchingRequest =
                this.GetFilterSettings() ?? new SearchingRequest // Not
                {
                    //IDsTasks = new UInt64[0],
                    //CreatedDate_Begin = new DateTime(0),
                    //CreatedDate_End = new DateTime(0),
                    //UpdatingDate_Begin = new DateTime(0),
                    //UpdatingDate_End = new DateTime(0),

                    Statuses = new Byte[0],

                    OrderOfFields = new String[0],
                    OrderOfValues = new Boolean[0]
                }
            };

            // Caption of fields for display
            String[] allFieldsOfTask =
                (model.SearchingRequest.OrderOfFields.Length == 0) ?
                typeof(Task_Queries).GetProperties().Select(p => p.Name).ToArray<String>() : model.SearchingRequest.OrderOfFields;
            model.AllFieldsOfTask = allFieldsOfTask.Select(f => new DisplayFieldOfTask(f)).ToList<DisplayFieldOfTask>();

            return View(model);
        }

        [HttpPost]
        public ActionResult SettingsOfFiltering(FilteringModel response)
        {
            response.SearchingRequest.Statuses = response.SearchingRequest.Statuses ?? new Byte[0];

            // Checking new searching request
            try
            {
                new Task_Queries[] { new Task_Queries() }.AsQueryable<Task_Queries>().Filtering(
                    null, //response.SearchingRequest.IDsTasks,
                    response.SearchingRequest.CreatedDate_Begin, response.SearchingRequest.CreatedDate_End,
                    response.SearchingRequest.UpdatingDate_Begin, response.SearchingRequest.UpdatingDate_End,
                    null, response.SearchingRequest.Statuses);
                //new RequestTasks { TasksView = storageData.GetTasksView, Request = response.SearchingRequest }.Searching();
            }
            catch (Exception e)
            {
                response.AllStatuses = storageData.GetStatuses.ToList<Status>();
                response.AllFieldsOfTask = response.SearchingRequest.OrderOfFields.Select(f => new DisplayFieldOfTask(f)).ToList<DisplayFieldOfTask>(); // Caption of fields for display
                response.ErrorMessage = e.Message;
                return View(response);
            }

            this.SetFilterSettings(response.SearchingRequest);
            return RedirectToAction("TasksList", "Tasks");
        }

        public ActionResult ResetFilter()
        {
            this.RemoveFilterSettings();
            return RedirectToAction("TasksList", "Tasks");
        }
    }
}
