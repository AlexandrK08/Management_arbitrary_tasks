using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArbitraryTasks;
using ArbitraryTasks.Storage;
using ArbitraryTasks.Entities;
using ArbitraryTasks.Extensions;
using Management_arbitrary_tasks.Models;
using Management_arbitrary_tasks.Utilities;

namespace Management_arbitrary_tasks.Controllers
{
    public class EnteringController : Controller
    {
        private IStorageData storageData;

        public EnteringController(IStorageData storageData)
        {
            this.storageData = storageData;
        }

        //
        // GET: /Entering/

        public ActionResult Index()
        {
            return RedirectToAction("Enter");
        }

        [HttpGet]
        public ActionResult Enter()
        {
            EnterModel model = new EnterModel
            {
                Users = storageData.GetUsers.ToList<User>(),
                CurrentUser = this.GetActiveUser(storageData) // Not
            };

            if (model.CurrentUser == null)
            {
                model.CurrentUser = new User { ID = 0, Name = "Выбор пользователя" };
                model.Users.Add(model.CurrentUser);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Enter(EnterModel response)
        {
            if (storageData.GetUsers.ExistUser(response.CurrentUserID))
            {
                this.SetUserID(response.CurrentUserID); // Not
                return RedirectToAction("TasksList", "Tasks");
            }
            else
            {
                response.Users = storageData.GetUsers.ToList<User>();
                response.CurrentUser = new User { ID = 0, Name = "Выбор пользователя" };
                response.Users.Add(response.CurrentUser);
                response.ErrorMessage = "Выберите пользователя для входа!";
                return View(response);
            }
        }

        public ActionResult Exit()
        {
            this.RemoveUserID();
            this.RemoveFilterSettings();
            return RedirectToAction("Enter");
        }
    }
}
