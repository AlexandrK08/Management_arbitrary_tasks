using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArbitraryTasks.Entities;
using System.ComponentModel.DataAnnotations;

namespace Management_arbitrary_tasks.Models
{
    public class EnterModel
    {
        // [Required(ErrorMessage = "")]
        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                CurrentUserID = (_CurrentUser != null) ? _CurrentUser.ID : 0;
            }
        }

        public UInt64 CurrentUserID { get; set; }

        public IList<User> Users { get; set; }

        public String ErrorMessage { get; set; }
    }
}
