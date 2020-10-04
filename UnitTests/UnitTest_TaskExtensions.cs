using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;
using ArbitraryTasks.Extensions;
//using ArbitraryTasks.TasksSearching;

namespace UnitTests
{
    [TestClass]
    public class UnitTest_TaskExtensions
    {
        [TestMethod]
        public void UnitTest_TaskViewExtensions_Filtering_01()
        {
            // Arrange
            List<Task_Queries> tasks = new List<Task_Queries>
            {
                new Task_Queries
                {
                    ID = 1,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 12),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 2,
                    Caption = "Task 02",
                    CreateDate = new DateTime(2020, 03, 05),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 3,
                    Caption = "Task 03",
                    CreateDate = new DateTime(2020, 04, 05, 15, 45, 10),
                    LastUpdateDate = new DateTime(2020, 04, 05, 16, 40, 20),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                }
            };
            IQueryable<Task_Queries> taskViews = tasks.AsQueryable();

            // Act
            Int32 countResults_01 = taskViews.GetByCreatedDate(new DateTime(2020, 03, 06), new DateTime(2020, 03, 12)).Count();
            Int32 countResults_02 = taskViews.GetByUpdatingDate(new DateTime(2020, 03, 13), new DateTime(2020, 03, 16)).Count();
            Int32 countResults_03 = taskViews.GetByStatuses(new Byte[] { 1 }).Count();
            Int32 countResults_04 = taskViews.GetByUpdatingDate(new DateTime(2020, 04, 05, 18, 20, 10), new DateTime(2020, 04, 05, 18, 20, 10)).Count();
            Int32 countResults_05 = taskViews.GetByUpdatingDate(new DateTime(2020, 04, 05, 18, 20, 10), new DateTime(2020, 04, 05, 18, 20, 10)).Count();
            Int32 countResults_06 = taskViews.GetByIDsTasks(new UInt64[] { 2 }).Count();
            Int32 countResults_07 = taskViews.GetByUsersCreators(new UInt64[] { 2 }).Count();

            // Assert
            Assert.AreEqual(1, countResults_01, "Неверное количество результатов в запросе 01");
            Assert.AreEqual(1, countResults_02, "Неверное количество результатов в запросе 02");
            Assert.AreEqual(1, countResults_03, "Неверное количество результатов в запросе 03");
            Assert.AreEqual(1, countResults_04, "Неверное количество результатов в запросе 04");
            Assert.AreEqual(1, countResults_05, "Неверное количество результатов в запросе 05");
            Assert.AreEqual(1, countResults_06, "Неверное количество результатов в запросе 06");
            Assert.AreEqual(2, countResults_07, "Неверное количество результатов в запросе 07");
        }

        [TestMethod]
        public void UnitTest_TaskViewExtensions_Filtering_02()
        {
            // Arrange
            List<Task_Queries> tasks = new List<Task_Queries>
            {
                new Task_Queries
                {
                    ID = 1,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 03, 01),
                    LastUpdateDate = new DateTime(2020, 03, 05),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 2,
                    Caption = "Task 02",
                    CreateDate = new DateTime(2020, 03, 15),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 3,
                    Caption = "Task 03",
                    CreateDate = new DateTime(2020, 03, 31),
                    LastUpdateDate = new DateTime(2020, 03, 31),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 4,
                    Caption = "Task 04",
                    CreateDate = new DateTime(2020, 04, 05),
                    LastUpdateDate = new DateTime(2020, 04, 05),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                }
            };
            IQueryable<Task_Queries> taskViews = tasks.AsQueryable();

            // Act
            Int32 countResults_01 = taskViews.Filtering(
                CreatedDate_Begin: new DateTime(2020, 03, 01), 
                CreatedDate_End: new DateTime(2020, 03, 31),
                UpdatingDate_Begin: new DateTime(2020, 03, 05),
                UpdatingDate_End: new DateTime(2020, 03, 16),
                IDsUsersOfCreators: new UInt64[1] { 1 }).Count();

            // Assert
            Assert.AreEqual(1, countResults_01, "Неверное количество результатов в запросе 01");
        }

        [TestMethod]
        public void FilteringTaskView_Sorting_01()
        {
            // Arrange
            List<Task_Queries> tasks = new List<Task_Queries>
            {
                new Task_Queries
                {
                    ID = 5,
                    Caption = "Task 04",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 3,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 04, 05),
                    LastUpdateDate = new DateTime(2020, 04, 05),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 1,
                    Caption = "Task 03",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 12),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 2,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 03, 05),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 4,
                    Caption = "Task 04",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 12),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
            };
            IQueryable<Task_Queries> taskViews = tasks.AsQueryable();

            // Act
            taskViews = taskViews.Sorting(new string[] { "CreateDate", "Caption", "LastUpdateDate" }, new Boolean[0]);
            List<Task_Queries> lTaskViews = taskViews.ToList<Task_Queries>();

            // Assert
            Assert.AreEqual(5, taskViews.Count(), "Неверное количество элементов");

            // 2 1 4 5 3
            Assert.AreEqual((UInt64)2, lTaskViews.ElementAt(0).ID, "Неверная дата в позиции 0");
            Assert.AreEqual((UInt64)1, lTaskViews.ElementAt(1).ID, "Неверная дата в позиции 1");
            Assert.AreEqual((UInt64)4, lTaskViews.ElementAt(2).ID, "Неверная дата в позиции 2");
            Assert.AreEqual((UInt64)5, lTaskViews.ElementAt(3).ID, "Неверная дата в позиции 3");
            Assert.AreEqual((UInt64)3, lTaskViews.ElementAt(4).ID, "Неверная дата в позиции 3");
        }

        [TestMethod]
        public void FilteringTaskView_Sorting_02()
        {
            // Arrange
            List<Task_Queries> tasks = new List<Task_Queries>
            {
                new Task_Queries
                {
                    ID = 5,
                    Caption = "Task 04",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 3,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 04, 05),
                    LastUpdateDate = new DateTime(2020, 04, 05),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 1,
                    Caption = "Task 03",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 12),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 2,
                    Caption = "Task 01",
                    CreateDate = new DateTime(2020, 03, 05),
                    LastUpdateDate = new DateTime(2020, 03, 15),
                    CreateUser = new User { ID = 2, Name = "User 02" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 1, Value = 0, Caption = "Открыта", Description = String.Empty }
                },
                new Task_Queries
                {
                    ID = 4,
                    Caption = "Task 04",
                    CreateDate = new DateTime(2020, 03, 09),
                    LastUpdateDate = new DateTime(2020, 03, 12),
                    CreateUser = new User { ID = 1, Name = "User 01" },
                    LastUpdateUser = new User { ID = 2, Name = "User 02" },
                    Status = new Status { ID = 2, Value = 1, Caption = "Взята", Description = String.Empty }
                },
            };
            IQueryable<Task_Queries> taskViews = tasks.AsQueryable();

            // Act
            taskViews = taskViews.Sorting(new string[] { "CreateDate", "Caption", "LastUpdateDate" }, new Boolean[3] { true, false, true });
            List<Task_Queries> lTaskViews = taskViews.ToList<Task_Queries>();

            // Assert
            Assert.AreEqual(5, taskViews.Count(), "Неверное количество элементов");

            // 3 1 5 4 2
            Assert.AreEqual((UInt64)3, lTaskViews.ElementAt(0).ID, "Неверная дата в позиции 0");
            Assert.AreEqual((UInt64)1, lTaskViews.ElementAt(1).ID, "Неверная дата в позиции 1");
            Assert.AreEqual((UInt64)5, lTaskViews.ElementAt(2).ID, "Неверная дата в позиции 2");
            Assert.AreEqual((UInt64)4, lTaskViews.ElementAt(3).ID, "Неверная дата в позиции 3");
            Assert.AreEqual((UInt64)2, lTaskViews.ElementAt(4).ID, "Неверная дата в позиции 3");
        }
    }
}
