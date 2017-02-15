using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GoodMorning
{
    [Serializable]
    public class Days
    {
        public List <Day> DaysList { get; set; }
        private string fileName;
        private int numberOfDaysBeforeToday = -31;
        public Days()
        {
            fileName = "days.xml";
            DaysList = new List<Day>();
            for(int i = numberOfDaysBeforeToday; i < 365; i++)
            {
                DateTime day = DateTime.Today;
                day = day.AddDays(i);
                DaysList.Add(new Day(day));
            }
        }
      
  
    }
}
