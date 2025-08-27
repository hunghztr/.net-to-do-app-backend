using System.ComponentModel.DataAnnotations;

namespace ToDoList.Utils.Attribute
{
    public class ValidDueDate : ValidationAttribute
    {
        public ValidDueDate() {
            ErrorMessage = "Due date must be in the future.";
        }
        public override bool IsValid(object dueDate)
        {
           if(dueDate is DateTime dateTime)
            {
                return dateTime >= DateTime.Now;
            }
              return false;
        }
    }
}
