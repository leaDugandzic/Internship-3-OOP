using ProjectManagerApp.Classes;
using ProjectManagerApp.Enum;

namespace ProjectManagerApp
{
    internal class Program
    {
        static Dictionary<Project, List<ProjectTask>> projects = new();
        static void Main(string[] args)
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

            //menu 
            while (true)
            {
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
                        AddNewProjectWithTasks();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteProject();
                        break;
                    case "4":
                        Console.Clear();
                        ShowTasksForNext7Days();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                //function for displaying all projects and their tasks
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

                //function for adding new project
                static void AddNewProjectWithTasks()
                {
                    Console.WriteLine("\nAdding a new project...");

                    string name = InputValidator.GetValidatedString("Enter project name: ");

                    if (projects.Keys.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"A project with the name '{name}' already exists. Cannot add duplicate projects.");
                        return;
                    }

                    string description = InputValidator.GetValidatedString("Enter project description: ");
                    DateTime startDate = InputValidator.GetValidatedDate("Enter start date (yyyy-MM-dd): ");
                    DateTime endDate = InputValidator.GetValidatedDate("Enter end date (yyyy-MM-dd): ");
                    InputValidator.GetValidateDateRange(startDate, endDate);
                    Status status = InputValidator.GetValidatedStatus("Enter status (Active, Paused, Completed): ");

                    Project newProject = new Project(name, description, startDate, endDate, status);
                    projects[newProject] = new List<ProjectTask>();

                    Console.Clear();
                    Console.WriteLine($"Project '{name}' added successfully!");

                    int addTasksChoice = InputValidator.GetValidatedNumber($"\nHow many tasks would you like to add in project '{name}'? ");

                    for (int i = 1; i <= addTasksChoice; i++)
                    {
                        Console.WriteLine($"Task {i} / {addTasksChoice}");
                        string taskName = InputValidator.GetValidatedString("Enter task name: ");

                        if (projects[newProject].Any(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine($"A task with the name '{taskName}' already exists in project '{name}'. Cannot add duplicate tasks.");
                            continue;
                        }

                        string taskDescription = InputValidator.GetValidatedString("Enter task description: ");
                        DateTime taskDeadline;

                        while (true)
                        {
                            taskDeadline = InputValidator.GetValidatedDate("Enter task deadline (yyyy-MM-dd): ");
                            if (taskDeadline < startDate || taskDeadline > endDate)
                            {
                                Console.WriteLine($"Invalid deadline. The task deadline must be between the project's start date ({startDate:yyyy-MM-dd}) and end date ({endDate:yyyy-MM-dd}).");
                                continue;
                            }
                            break;
                        }

                        Status taskStatus = InputValidator.GetValidatedStatus("Enter task status (Active, Paused, Completed): ");
                        int taskDuration = InputValidator.GetValidatedNumber("Enter task duration (in minutes): ");

                        ProjectTask newTask = new ProjectTask(taskName, taskDescription, taskDeadline, taskStatus, taskDuration, newProject);
                        projects[newProject].Add(newTask);

                        Console.Clear();
                        Console.WriteLine($"Task '{taskName}' added to project '{name}' successfully!");

                    }

                    Console.WriteLine($"\nProject '{name}' with tasks has been finalized.");
                }

                //function for deleting project
                static void DeleteProject()
                {
                    foreach (var project in projects)
                    {
                        Console.WriteLine($"Project: {project.Key.Name} ({project.Key.Status})");
                    }

                    Project projectToDelete = null;

                    while (projectToDelete == null)
                    {
                        string projectName = InputValidator.GetValidatedString("Enter the name of the project to delete: ");

                        projectToDelete = projects.Keys.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));

                        if (projectToDelete == null)
                        {
                            Console.WriteLine($"Project '{projectName}' not found. Please try again.");
                        }
                    }

                    Console.Write($"Are you sure you want to delete the project '{projectToDelete.Name}'? (y/n): ");
                    string confirmation = Console.ReadLine();
                    if (!confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Clear();
                        Console.WriteLine("Delete project cancelled.");
                        return;
                    }

                    projects.Remove(projectToDelete);
                    Console.Clear();
                    Console.WriteLine($"Project '{projectToDelete.Name}' deleted successfully.");
                }

                //function for showing tasks in next 7 days
                static void ShowTasksForNext7Days()
                {
                    DateTime now = DateTime.Now;
                    DateTime future = now.AddDays(7);

                    Console.WriteLine("\nTasks due in the next 7 days:");
                    foreach (var project in projects)
                    {
                        foreach (var task in project.Value.Where(t => t.Deadline >= now && t.Deadline <= future))
                        {
                            Console.WriteLine($"Task: {task.Name} (Project: {project.Key.Name}, Deadline: {task.Deadline:yyyy-MM-dd})");
                        }
                    }
                }
            }
        }
    }
}
