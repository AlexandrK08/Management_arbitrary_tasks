using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ArbitraryTasks.Storage;
using ArbitraryTasks.Entities;
using ArbitraryTasks.EntitiesQueries;
using ArbitraryTasks.Extensions;

namespace UnitTests
{
    [TestClass]
    public class UnitTest_EntitiesUtility
    {
        [TestMethod]
        public void EntitiesUtility_AddUser()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();

            // Act
            UInt64 newID = storageData.AddUser(new User { Name = "User01" });
            Int32 countUsers = storageData.GetUsers.Count();
            User newGetUser = storageData.GetUsers.First();

            // Assert
            Assert.AreEqual(1, countUsers, "Пользователь НЕ добавлен");
            Assert.AreEqual((UInt64)1, newID, "Идентификатор пользователю НЕ присвоен");
            Assert.AreEqual((UInt64)1, newGetUser.ID, "Идентификаторы НЕ совпадают");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_DuplicationOfUsers()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddUser(new User { Name = "User01" });

            // Act
            storageData.AddUser(new User { Name = "User01" });

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_BadNameOfUser()
        {
            // Arrange
            User newUser = new User();

            // Act
            newUser.Name ="012345678901234567890123456";

            // Assert
        }

        [TestMethod]
        public void EntitiesUtility_GettingUsers()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });
            storageData.AddUser(new User { Name = "User04" });
            storageData.AddUser(new User { Name = "User05" });

            // Act
            Int32 quantity_01 = storageData.GetUsers.Count();
            Int32 quantity_02 = storageData.GetUsers.Where(u => u.ID ==3).Count();
            User getUser_01 = storageData.GetUsers.GetUser(5);

            // Assert
            Assert.AreEqual(5, quantity_01, "Не все пользователи вернулись");
            Assert.AreEqual(1, quantity_02, "Пользователи по ID НЕ возвращены");
            Assert.AreEqual("User05", getUser_01.Name, "Пользователь по ID возвращен");
        }

        [TestMethod]
        public void EntitiesUtility_ExistUsers()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });
            storageData.AddUser(new User { Name = "User04" });
            storageData.AddUser(new User { Name = "User05" });

            // Act
            Boolean exist_01 = storageData.GetUsers.ExistUser(4);
            Boolean exist_02 = storageData.GetUsers.ExistUser(6);
            Boolean exist_03 = storageData.GetUsers.ExistUser("User01");
            Boolean exist_04 = storageData.GetUsers.ExistUser("User06");

            // Assert
            Assert.AreEqual(true, exist_01, "Пользователь по ID НЕ существует");
            Assert.AreEqual(false, exist_02, "Пользователь по не существующему ID СУЩЕСТВУЕТ");
            Assert.AreEqual(true, exist_03, "Пользователь по Name НЕ существует");
            Assert.AreEqual(false, exist_04, "Пользователь по не существующему Name СУЩЕСТВУЕТ");
        }

        [TestMethod]
        public void EntitiesUtility_AddStatus()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();

            // Act
            UInt64 newID = storageData.AddStatus(new Status { Value = 0, Caption = "Status 01", Description = String.Empty });
            Int32 countStatuses = storageData.GetStatuses.Count();
            Status newGetStatus = storageData.GetStatuses.First();

            // Assert
            Assert.AreEqual(1, countStatuses, "Статус НЕ добавлен");
            Assert.AreEqual((UInt64)1, newID, "Идентификатор статусу НЕ присвоен");
            Assert.AreEqual((UInt64)1, newGetStatus.ID, "Идентификаторы НЕ совпадают");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_DuplicationOfStatus()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 1, Caption = "Status 01", Description = String.Empty });

            // Act
            storageData.AddStatus(new Status { Value = 1, Caption = "Status 02", Description = String.Empty });

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_BadNameOfStatus()
        {
            // Arrange
            Status newStatus = new Status();

            // Act
            newStatus.Caption = "012345678901234567890123456";

            // Assert
        }

        [TestMethod]
        public void EntitiesUtility_GettingStatuses()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 100, Caption = "Status 01", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 101, Caption = "Status 02", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 102, Caption = "Status 03", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 103, Caption = "Status 04", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 104, Caption = "Status 05", Description = String.Empty });

            // Act
            Int32 quantity_01 = storageData.GetStatuses.Count();
            Int32 quantity_02 = storageData.GetStatuses.Where(s => s.ID == 2).Count();
            Int32 quantity_03 = storageData.GetStatuses.Where(s => s.Value == 103).Count();
            Status getStatus_01 = storageData.GetStatuses.GetStatus((UInt64)4);
            Status getStatus_02 = storageData.GetStatuses.GetStatus((Byte)104);

            // Assert
            Assert.AreEqual(5, quantity_01, "Не все статусы вернулись");
            Assert.AreEqual(1, quantity_02, "Статусы по ID НЕ возвращены");
            Assert.AreEqual(1, quantity_03, "Статусы по значению НЕ возвращены");
            Assert.AreEqual("Status 04", getStatus_01.Caption, "Статус по ID НЕ возвращен");
            Assert.AreEqual("Status 05", getStatus_02.Caption, "Статус по значению НЕ возвращен");
        }

        [TestMethod]
        public void EntitiesUtility_ExistStatus()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 100, Caption = "Status 01", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 101, Caption = "Status 02", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 102, Caption = "Status 03", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 103, Caption = "Status 04", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 104, Caption = "Status 05", Description = String.Empty });

            // Act
            Boolean exist_01 = storageData.GetStatuses.ExistStatus((UInt64)3);
            Boolean exist_02 = storageData.GetStatuses.ExistStatus((Byte)103);
            Boolean exist_03 = storageData.GetStatuses.ExistStatus((UInt64)6);
            Boolean exist_04 = storageData.GetStatuses.ExistStatus((Byte)105);

            // Assert
            Assert.AreEqual(true, exist_01, "Статус по ID НЕ существует");
            Assert.AreEqual(true, exist_02, "Статус по значению НЕ существует");
            Assert.AreEqual(false, exist_03, "Статус по не существующему ID СУЩЕСТВУЕТ");
            Assert.AreEqual(false, exist_04, "Статус по не существующему значению СУЩЕСТВУЕТ");
        }

        [TestMethod]
        public void EntitiesUtility_AddTask()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 0, Caption = "Открыта", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 1, Caption = "Взята", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 2, Caption = "Решена", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 3, Caption = "Возврат", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 4, Caption = "Закрыта", Description = String.Empty });
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });

            // Act
            Task newTask = new Task { Caption = "Task01"};
            TaskChange newTaskChange = new TaskChange{ Comment = "Comment01", UserID = 1};
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);
            Int32 countTasks = storageData.GetTasks.Count();
            Task newGetTask = storageData.GetTasks.First();
            Int32 countChanges = storageData.GetTaskChanges.Count();

            // Assert
            Assert.AreEqual(1, countTasks, "Заявка НЕ добавлен");
            Assert.AreEqual(1, countChanges, "Изменение НЕ добавлен");
            Assert.AreEqual((UInt64)1, newTask.ID, "Идентификатор заявке НЕ присвоен");
            Assert.AreEqual((UInt64)1, newGetTask.ID, "Идентификаторы НЕ совпадают");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_BadCaptionOfTask()
        {
            // Arrange
            String caption = String.Empty;
            for (Int32 i = 1; i < 52; i++)
            {
                caption += "0";
            }

            // Act
            new Task { Caption = caption };

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_BadCommentOfTask()
        {
            // Arrange
            String comment = String.Empty;
            for (Int32 i = 1; i < 258; i++)
            {
                comment += "0";
            }

            // Act
            new TaskChange { Comment = comment };

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EntitiesUtility_BadUserOfTask()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 0, Caption = "Открыта", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 1, Caption = "Взята", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 2, Caption = "Решена", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 3, Caption = "Возврат", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 4, Caption = "Закрыта", Description = String.Empty });
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });

            // Act
            Task newTask = new Task { Caption = "Caption 01" };
            TaskChange newTaskChange = new TaskChange { Comment = "Comment 01", UserID = 5 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            // Assert
        }

        [TestMethod]
        public void EntitiesUtility_GettingTasks()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 0, Caption = "Открыта", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 1, Caption = "Взята", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 2, Caption = "Решена", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 3, Caption = "Возврат", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 4, Caption = "Закрыта", Description = String.Empty });
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });

            Task newTask;
            TaskChange newTaskChange;

            newTask = new Task { Caption = "Caption 01" };
            newTaskChange = new TaskChange { Comment = "Comment 01", UserID = 1};
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 02" };
            newTaskChange = new TaskChange { Comment = "Comment 02", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 03" };
            newTaskChange = new TaskChange { Comment = "Comment 03", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 04" };
            newTaskChange = new TaskChange { Comment = "Comment 04", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 05" };
            newTaskChange = new TaskChange { Comment = "Comment 05", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            // Act
            Task_Queries getTask_01 = storageData.GetTasksQueries().GetByID(3);

            // Assert
            Assert.AreEqual("Caption 03", getTask_01.Caption, "Заявка НЕ вернулась по ID");
        }

        [TestMethod]
        public void EntitiesUtility_ExistTasks()
        {
            // Arrange
            IStorageData storageData = new Storage.StorageData_Test();
            storageData.AddStatus(new Status { Value = 0, Caption = "Открыта", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 1, Caption = "Взята", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 2, Caption = "Решена", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 3, Caption = "Возврат", Description = String.Empty });
            storageData.AddStatus(new Status { Value = 4, Caption = "Закрыта", Description = String.Empty });
            storageData.AddUser(new User { Name = "User01" });
            storageData.AddUser(new User { Name = "User02" });
            storageData.AddUser(new User { Name = "User03" });

            Task newTask;
            TaskChange newTaskChange;

            newTask = new Task { Caption = "Caption 01" };
            newTaskChange = new TaskChange { Comment = "Comment 01", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 02" };
            newTaskChange = new TaskChange { Comment = "Comment 02", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 03" };
            newTaskChange = new TaskChange { Comment = "Comment 03", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 04" };
            newTaskChange = new TaskChange { Comment = "Comment 04", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            newTask = new Task { Caption = "Caption 05" };
            newTaskChange = new TaskChange { Comment = "Comment 05", UserID = 1 };
            storageData.SaveNewTaskAndChange(newTask, newTaskChange);

            // Act
            Boolean exist_01 = storageData.GetTasksQueries().ExistTask(4);
            Boolean exist_02 = storageData.GetTasksQueries().ExistTask(6);

            // Assert
            Assert.AreEqual(true, exist_01, "Заявка по ID НЕ существует");
            Assert.AreEqual(false, exist_02, "Заявка по не существующему ID СУЩЕСТВУЕТ");
        }
    }
}
