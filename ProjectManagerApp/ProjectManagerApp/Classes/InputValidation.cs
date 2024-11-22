using ProjectManagerApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public static class InputValidator
    {
        public static string GetValidatedString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please enter a non-empty value.");
                    continue;
                }

                if (int.TryParse(input, out int _))
                {
                    Console.WriteLine("Invalid input. Please don't use numbers for names.");
                    continue;
                }

                return input; 
            }
        }

        public static DateTime GetValidatedDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out DateTime result))
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in the format yyyy-MM-dd.");
                    continue;
                }

                if (result.Year < DateTime.Now.Year || result.Year > DateTime.Now.Year + 1)
                {
                    Console.WriteLine($"Invalid date. The year cannot be later than {DateTime.Now.Year}.");
                    continue;
                }

                return result; 
            }
        }

        public static bool GetValidateDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                Console.WriteLine($"Invalid date range. End date ({endDate:yyyy-MM-dd}) cannot be earlier than start date ({startDate:yyyy-MM-dd}).");
                return false;
            }

            return true; 
        }
        
        public static Status GetValidatedStatus(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (Status.TryParse(Console.ReadLine(), true, out Status result))
                    return result;

                Console.WriteLine("Invalid status. Please enter one of the following: Active, Paused, Completed.");
            }
        }

        public static int GetValidatedNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= 1)
                    return result;

                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }

        public static bool IsProjectCompleted(Project project, Dictionary<Project, List<ProjectTask>> projects)
        {
            if (project.Status == Status.Completed)
            {
                Console.WriteLine($"Project '{project.Name}' is already marked as Completed. You cannot change the status of a completed project.");
                return true; 
            }

            bool allTasksCompleted = projects[project].All(task => task.Status == Status.Completed);
            if (allTasksCompleted)
            {
                project.Status = Status.Completed; 
                Console.WriteLine($"All tasks for project '{project.Name}' are completed. Project status automatically updated to 'Completed'.");
                return true; 
            }

            return false; // Projekt nije završen
        }

        public static void MarkAllTasksCompleted(Project project, Dictionary<Project, List<ProjectTask>> projects)
        {
            foreach (var task in projects[project])
            {
                task.Status = Status.Completed;
            }
            Console.WriteLine($"All tasks for project '{project.Name}' have been marked as Completed.");
        }

    }

}
