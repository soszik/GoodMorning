using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace GoodMorning
{
    [Serializable]
    public class Day
    {
        public DateTime Date { get; set; }
        public List<Task> Tasks { get; set; }
        public Day() { }
        public Day(DateTime date)
        {
            Tasks = new List<Task>();
            Date = date;
            DateTime hour;
        }

    }
}
