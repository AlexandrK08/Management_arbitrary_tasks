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

namespace Management_arbitrary_tasks.Utilities
{
    public static class WorkingWithCookie
    {
        public static String KeyOfUserID = "UserID"; // From config
        public static String KeyOfFilterSettings = "FilterSettings"; // From config

        #region ' UserID '

        public static void SetUserID(this Controller controller, UInt64 userID)
        {
            HttpCookie newUserID = new HttpCookie(String.Empty)
            {
                Name = KeyOfUserID,
                Value = userID.ToString(),
                Expires = DateTime.Now.AddMonths(1)
            };
            controller.Response.Cookies.Set(newUserID);
        }

        public static UInt64? GetUserID(this Controller controller)
        {
            UInt64? userID = null;
            HttpCookie requestUserID = controller.Request.Cookies.Get(KeyOfUserID);
            if (requestUserID != null && !String.IsNullOrEmpty(requestUserID.Value))
            {
                userID = Convert.ToUInt64(requestUserID.Value);
                requestUserID.Expires = DateTime.Now.AddMonths(1);
                controller.Response.Cookies.Set(requestUserID);
            }

            return userID;
        }

        public static void RemoveUserID(this Controller controller)
        {
            controller.Response.Cookies.Set(new HttpCookie(KeyOfUserID) { Expires = DateTime.Now.AddDays(-1) });
        }

        #endregion

        public static User GetActiveUser(this Controller controller, IStorageData storageData)
        {
            UInt64? userID = GetUserID(controller);
            return (userID != null) ? storageData.GetUsers.GetUser(Convert.ToUInt64(userID)) : null;
        }

        #region ' FilterSettings '

        public static void SetFilterSettings(this Controller controller, SearchingRequest settings)
        {
            if (settings == null)
            {
                throw new Exception("У поискового запроса нет значения");
            }

            // Serialization of settings
            String xmlSettings64 = String.Empty;
            System.IO.MemoryStream settingsStream = new System.IO.MemoryStream();
            new System.Xml.Serialization.XmlSerializer(typeof(SearchingRequest)).Serialize(settingsStream, settings);
            xmlSettings64 = Convert.ToBase64String(settingsStream.ToArray());

            // Saving settings in Cookie
            HttpCookie newFilterSettings = new HttpCookie(String.Empty)
            {
                Name = KeyOfFilterSettings,
                Value = xmlSettings64,
                Expires = DateTime.Now.AddMonths(1)
            };
            controller.Response.Cookies.Set(newFilterSettings);
        }

        public static SearchingRequest GetFilterSettings(this Controller controller)
        {
            // Getting settings from Cookie
            String xmlSettings64 = String.Empty;
            HttpCookie requestFilterSettings = controller.Request.Cookies.Get(KeyOfFilterSettings);
            if (requestFilterSettings != null && !String.IsNullOrEmpty(requestFilterSettings.Value))
            {
                xmlSettings64 = requestFilterSettings.Value;
                requestFilterSettings.Expires = DateTime.Now.AddMonths(1);
                controller.Response.Cookies.Set(requestFilterSettings);
            }

            // Deserialize of settings
            SearchingRequest searchingRequest = null;
            if (!String.IsNullOrEmpty(xmlSettings64))
            {
                try
                {
                    searchingRequest =
                        (SearchingRequest)
                        (new System.Xml.Serialization.XmlSerializer(typeof(SearchingRequest)))
                        .Deserialize(new System.IO.MemoryStream(Convert.FromBase64String(xmlSettings64)));
                }
                catch
                {
                    RemoveFilterSettings(controller);
                }
            }

            return searchingRequest;
        }

        public static void RemoveFilterSettings(this Controller controller)
        {
            controller.Response.Cookies.Set(new HttpCookie(KeyOfFilterSettings) { Expires = DateTime.Now.AddDays(-1) });
        }

        #endregion
    }
}
