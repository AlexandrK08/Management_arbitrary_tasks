using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;

namespace Management_arbitrary_tasks.HtmlHelpers
{
    public static class TasksListHelpers
    {
        public static MvcHtmlString ManagementLink(this HtmlHelper html, String caption, String href)
        {
            TagBuilder tagA = new TagBuilder("a");
            tagA.InnerHtml = caption;
            tagA.MergeAttribute("href", href);
            return MvcHtmlString.Create(tagA.ToString());
        }

        public static String ConvertingStatusToColor(this HtmlHelper html, Byte status)
        {
            // Selection color
            String color = String.Empty;
            switch (status)
            {
                case 0:
                    color = "orange";
                    break;
                case 1:
                    color = "yellow";
                    break;
                case 2:
                    color = "green";
                    break;
                case 3:
                    color = "red";
                    break;
                case 4:
                    color = "grey";
                    break;
            }

            return color;
        }

        public static String ConvertingNameEventToCaption(this HtmlHelper html, String nameEvent)
        {
            // Selection caption
            String captionOfEvent = String.Empty;
            switch (nameEvent)
            {
                case "AddTask":
                    captionOfEvent = "Добавить";
                    break;
                case "TakingTask":
                    captionOfEvent = "Взять";
                    break;
                case "ReassignedTask":
                    captionOfEvent = "Забрать себе";
                    break;
                case "SolutionTask":
                    captionOfEvent = "Решил";
                    break;
                case "ReturnTask":
                    captionOfEvent = "Вернуть";
                    break;
                case "ClosingTask":
                    captionOfEvent = "Закрыть";
                    break;
                case "CommentingTask":
                    captionOfEvent = "Комментировать";
                    break;
            }

            return captionOfEvent;
        }

        public static String ConvertingNameOfFieldToCaption(this HtmlHelper html, String nameOfField)
        {
            String captionOfField = String.Empty;
            switch (nameOfField.ToLower())
            {
                case "id":
                    captionOfField = "ID";
                    break;
                case "caption":
                    captionOfField = "Заголовок";
                    break;
                case "createdate":
                    captionOfField = "Создана";
                    break;
                case "datechange":
                    captionOfField = "Создано";
                    break;
                case "createuser":
                    captionOfField = "Автор";
                    break;
                case "lastupdatedate":
                    captionOfField = "Обновлена";
                    break;
                case "lastupdateuser":
                    captionOfField = "Редактировал";
                    break;
                case "status":
                    captionOfField = "Статус";
                    break;
                case "comment":
                    captionOfField = "Комментарий";
                    break;
            }

            return captionOfField;
        }
    }
}
