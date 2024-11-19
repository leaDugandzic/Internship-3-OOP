using ProjectManagerApp.Classes;

namespace ProjectManagerApp
{
    internal class Program
    {
        static Dictionary<Project, List<ProjectTask>> projects = new();
        static void Main(string[] args)
        {
            //menu 
            while (true)
            {
                //InitializeProjectsAndTasks();

                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1 Display all projects and tasks");
                Console.WriteLine("2 Add a new project");
                Console.WriteLine("3 Delete a project");
                Console.WriteLine("4 Show tasks due in the next 7 days");
                Console.WriteLine("5 Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //DisplayAll();
                        break;
                    case "2":
                        //AddNewProject();
                        break;
                    case "3":
                        //DeleteProject();
                        break;
                    case "4":
                        //ShowTasksForNext7Days();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                static void InitializeProjectsAndTasks()
                {
                    Project websiteLaunch = new Project("Website Launch", "Launch the new company website", DateTime.Now, DateTime.Now.AddDays(60), Enum.Status.Active);
                    Project mobileApp = new Project("Mobile App Development", "Develop mobile app for company", DateTime.Now.AddDays(-15), DateTime.Now.AddDays(45), Enum.Status.Paused);

                    ProjectTask designPhase = new ProjectTask("Design Phase", "Complete the website design", DateTime.Now.AddDays(10), Enum.Status.Completed, 480, websiteLaunch);
                    ProjectTask contentCreation = new ProjectTask("Content Creation", "Write and edit website content", DateTime.Now.AddDays(20), Enum.Status.Active, 600, websiteLaunch);
                    ProjectTask testing = new ProjectTask("Testing", "Test website functionality", DateTime.Now.AddDays(50), Enum.Status.Active, 300, websiteLaunch);

                    ProjectTask prototype = new ProjectTask("Prototype Development", "Develop the app prototype", DateTime.Now.AddDays(5), Enum.Status.Active, 720, mobileApp);
                    ProjectTask bugFixing = new ProjectTask("Bug Fixing", "Fix critical bugs in the app", DateTime.Now.AddDays(25), Enum.Status.Paused, 200, mobileApp);

                    projects[websiteLaunch] = new List<ProjectTask> { designPhase, contentCreation, testing };
                    projects[mobileApp] = new List<ProjectTask> { prototype, bugFixing };
                }
            }
        }
    }
}
