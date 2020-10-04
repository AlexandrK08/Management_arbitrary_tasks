using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitraryTasks.Utilities
{
    public static class Validations
    {
        public static void CheckingText(String text, Int32 maxLength, String textOfError)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text) || text.Length > maxLength)
            {
                throw new Exception(textOfError);
            }
        }

        public static void CheckingDates(DateTime? begin, DateTime? end, String textOfError)
        {
            if (begin != null && end != null && begin.Value.Date > end.Value.Date)
            {
                throw new Exception(textOfError);
            }
        }
    }
}
