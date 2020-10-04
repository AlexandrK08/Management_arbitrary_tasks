using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ArbitraryTasks.Entities;
using ArbitraryTasks.Manipulations;

namespace UnitTests
{
    [TestClass]
    public class UnitTest_CreateTaskChange
    {
        [TestMethod]
        public void CreateTaskChange_Scenario_01()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта" },
                    new Status { ID = 2, Value = 1, Caption = "Взята" },
                    new Status { ID = 3, Value = 2, Caption = "Решена" },
                    new Status { ID = 4, Value = 3, Caption = "Возврат" },
                    new Status { ID = 5, Value = 4, Caption = "Закрыта" }
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 taking task 1";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 commenting task 1";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 commenting task 1";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 4;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 solution task 1";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 5;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 return task 1";
            createTaskChange.Return();
            createTaskChange.NewChange.ID = 6;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 taking task 1";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 7;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 solution task 1";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 8;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 closing task 1";
            createTaskChange.Closing();
            createTaskChange.NewChange.ID = 9;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        public void CreateTaskChange_Scenario_02()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 3;
            createTaskChange.Comment = "User3 taking task 2";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "Usre1 commenting task 2";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 3;
            createTaskChange.Comment = "User3 commenting task 2";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 4;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 3;
            createTaskChange.Comment = "User3 solution task 2";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 5;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 return task 2";
            createTaskChange.Return();
            createTaskChange.NewChange.ID = 6;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 3;
            createTaskChange.Comment = "User3 taking task 2";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 7;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 reassigned task 2";
            createTaskChange.Reassigned();
            createTaskChange.NewChange.ID = 8;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 commenting task 2";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 9;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 commenting task 2";
            createTaskChange.Commenting(); // Error!
            createTaskChange.NewChange.ID = 10;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 solution task 2";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 11;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 closing task 2";
            createTaskChange.Closing();
            createTaskChange.NewChange.ID = 12;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateTaskChange_Scenario_03()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 3, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 taking task 3";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.Comment = "User2 return task 3";
            createTaskChange.Return();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateTaskChange_Scenario_04()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 3;
            createTaskChange.Comment = "User3 taking task 1";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 closing task 1";
            createTaskChange.Closing();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        public void CreateTaskChange_Scenario_05()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 taking task 1";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 solution task 1";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        public void CreateTaskChange_Scenario_06()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User2 taking task 1";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "User1 commenting task 1";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User1 solution task 1";
            createTaskChange.Solution();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }

        [TestMethod]
        public void CreateTaskChange_Scenario_07()
        {
            // Arrange
            CreateTaskChange createTaskChange = new CreateTaskChange
            {
                Statuses = new List<Status>()
                {
                    new Status { ID = 1, Value = 0, Caption = "Открыта"},
                    new Status { ID = 2, Value = 1, Caption = "Взята"},
                    new Status { ID = 3, Value = 2, Caption = "Решена"},
                    new Status { ID = 4, Value = 3, Caption = "Возврат"},
                    new Status { ID = 5, Value = 4, Caption = "Закрыта"}
                }.AsQueryable<Status>(),
                TaskChanges = new List<TaskChange> { new TaskChange { ID = 1, StatusID = 1, UserID = 1, Comment = "root Comment" } }
            };

            // Act
            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User3 taking task 2";
            createTaskChange.Taking();
            createTaskChange.NewChange.ID = 2;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 1;
            createTaskChange.Comment = "Usre1 commenting task 2";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 3;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            createTaskChange.UserID = 2;
            createTaskChange.Comment = "User3 commenting task 2";
            createTaskChange.Commenting();
            createTaskChange.NewChange.ID = 4;
            createTaskChange.TaskChanges.Add(createTaskChange.NewChange);

            // Assert
        }
    }
}
