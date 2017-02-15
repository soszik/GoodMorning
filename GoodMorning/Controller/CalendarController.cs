using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GoodMorning
{
    public class CalendarController
    {
        private MainWindow window;
        public CalendarController(MainWindow mainwin )
        {
            window = mainwin;
        }

        public void AddTask(Day currentDay, int hour, int min, string tittle, string description)
        {
            if (!currentDay.Tasks.Exists(h => h.Time.Equals(currentDay.Date.AddMinutes(hour * 60 + min))))
                currentDay.Tasks.Add(new Task(currentDay.Date.AddMinutes(hour * 60 + min), tittle, description));
            else
                System.Windows.MessageBox.Show("There is already the task at the same time");
        }
        public void EditTask(Day currentDay, Task task, int hour, int min, string tittle, string description)
        {
            if (hour == task.Time.Hour && min == task.Time.Minute)
            {
                task.Time = new DateTime(task.Time.Year, task.Time.Month, task.Time.Day, hour, min,0);
                task.Description = description;
                task.Tittle = tittle;
               /* currentDay.Tasks.Add(new Task(currentDay.Date.AddMinutes(hour * 60 + min), tittle, description));
                currentDay.Tasks.Remove(task);*/
            }
            else if (!currentDay.Tasks.Exists(h => h.Time.Equals(currentDay.Date.AddMinutes(hour * 60 + min))))
            {
                task.Time = new DateTime(task.Time.Year, task.Time.Month, task.Time.Day, hour, min, 0);
                task.Description = description;
                task.Tittle = tittle;
            }
            else
                System.Windows.MessageBox.Show("There is already the task at the same time");
        }

        public void DeleteTask(Day currentDay, Task task)
        {
            if(task != null)
            {
                currentDay.Tasks.Remove(task);
            }
        }

        public void SaveCalendar(Days days)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Day>));
            List<Day> dl = days.DaysList;
            File.WriteAllText("WeeksData.xml", String.Empty);
            using (XmlWriter writer = XmlWriter.Create("WeeksData.xml"))
            {
                serializer.Serialize(writer, dl);
            }
        }

        public Days LoadCalendar()
        {
            Days days = new Days();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Day>));
            TextReader reader = new StreamReader("WeeksData.xml");
            object obj = deserializer.Deserialize(reader);
            List<Day> XmlData = (List<Day>)obj;
            days.DaysList = XmlData;

            return days;
        }
    }
}
