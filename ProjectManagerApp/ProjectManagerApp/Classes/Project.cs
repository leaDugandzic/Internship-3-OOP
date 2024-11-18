using ProjectManagerApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set;  } 
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }

        public Project(string name, string description, DateTime startDate, DateTime endDate, Status status)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

        public void ShowProjectDetails()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Start Date: {StartDate.ToShortDateString()}");
            Console.WriteLine($"End Date: {EndDate.ToShortDateString()}");
            Console.WriteLine($"Status: {Status}");
        }
    }
}
