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
                InitializeProjectsAndTasks();

                string menu = @"
    Main Menu:
    1 Display all projects and tasks
    2 Add a new project
    3 Delete a project
    4 Show tasks due in the next 7 days
    0 Exit";

                Console.WriteLine(menu);
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        Console.Clear();
                        DislayAll();
                        break;
                    case "2":
                        Console.Clear();
                        //AddNewProject();
                        break;
                    case "3":
                        Console.Clear();
                        //DeleteProject();
                        break;
                    case "4":
                        Console.Clear();
                        //ShowTasksForNext7Days();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                //inicijalizacija podataka za projekte i zadatke
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
                //prikaz svih projekata
                static void DislayAll()
                {
                    foreach (var project in projects)
                    {
                        Console.WriteLine($"Project: {project.Key.Name} ({project.Key.Status})");
                        foreach (var task in project.Value)
                        {
                            Console.WriteLine($"  - Task: {task.Name} (Status: {task.Status}, Deadline: {task.Deadline.ToShortDateString()})");
                        }
                    }
                }
            }
        }
    }
}
