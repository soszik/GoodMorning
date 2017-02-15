using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMorning
{
    [Serializable]
    public class Task
    {
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public Task(DateTime time)
        {
            Time = time;
            Tittle = "";
            Description = "";
        }
        public Task() { }
   
        public Task(DateTime time, string tittle, string description)
        {
            Time = time;
            Tittle = tittle;
            Description = description;
        }

       
        
    }
}
