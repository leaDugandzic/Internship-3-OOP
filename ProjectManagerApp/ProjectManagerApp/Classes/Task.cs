using ProjectManagerApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
        public int Duration { get; set; }
        public Project RelatedProject { get; set; }
        public Task(string name, string description, DateTime deadline, Status status, int duration, Project relatedProject) {
            Name = name;
            Description = description;
            Deadline = deadline;
            Status = status;
            Duration = duration;
            RelatedProject = relatedProject;
        }
        public void ShowProjectDetails()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Deadline: {Deadline.ToShortDateString()}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Duration: {Duration}");
            Console.WriteLine($"Related Project: {RelatedProject}");
        }
    }
}
