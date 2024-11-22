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

            ProjectTask designPhase = new ProjectTask("Design Phase", "Complete the website design", DateTime.Now.AddDays(10), Enum.Status.Completed, 480, websiteLaunch);
            ProjectTask contentCreation = new ProjectTask("Content Creation", "Write and edit website content", DateTime.Now.AddDays(20), Enum.Status.Active, 600, websiteLaunch);
            ProjectTask testing = new ProjectTask("Testing", "Test website functionality", DateTime.Now.AddDays(50), Enum.Status.Active, 300, websiteLaunch);

            Project mobileApp = new Project("Mobile App Development", "Develop mobile app for company", DateTime.Now.AddDays(-15), DateTime.Now.AddDays(45), Enum.Status.Paused);

            ProjectTask prototype = new ProjectTask("Prototype Development", "Develop the app prototype", DateTime.Now.AddDays(5), Enum.Status.Active, 720, mobileApp);
            ProjectTask bugFixing = new ProjectTask("Bug Fixing", "Fix critical bugs in the app", DateTime.Now.AddDays(25), Enum.Status.Paused, 200, mobileApp);

            Project cloudMigration = new Project("Cloud Migration", "Migrate all company data to the cloud", DateTime.Now.AddDays(-30), DateTime.Now.AddDays(90), Enum.Status.Completed);

            ProjectTask planning = new ProjectTask("Planning", "Develop a migration strategy", DateTime.Now.AddDays(-20), Enum.Status.Completed, 360, cloudMigration);
            ProjectTask dataBackup = new ProjectTask("Data Backup", "Create a secure backup of all data before migration", DateTime.Now.AddDays(5), Enum.Status.Completed, 480, cloudMigration); ;
            ProjectTask migration = new ProjectTask("Migration Execution", "Migrate data to the cloud", DateTime.Now.AddDays(45), Enum.Status.Completed, 720, cloudMigration);
            ProjectTask testingAndVerification = new ProjectTask("Testing and Verification", "Test the migrated data for integrity and functionality", DateTime.Now.AddDays(75), Enum.Status.Completed, 300, cloudMigration);


            projects[websiteLaunch] = new List<ProjectTask> { designPhase, contentCreation, testing };
            projects[mobileApp] = new List<ProjectTask> { prototype, bugFixing };
            projects[cloudMigration] = new List<ProjectTask> {  planning, dataBackup, migration, testingAndVerification };

            //menu 
            while (true)
            {
                string menu = @"
Main Menu:
1 Display all projects and tasks
2 Add a new project
3 Delete a project
4 Show tasks due in the next 7 days
5 Filter projects by status
6 Manage project
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
                    case "5":
                        Console.Clear();
                        FilterProjectsByStatus();
                        break;
                    case "6":
                        Console.Clear();
                        ManageProject();
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

                //function for filtering projects by status
                static void FilterProjectsByStatus()
                {
                    while (true)
                    {
                        Console.WriteLine("Select status to filter by: 1 - Active, 2 - Paused, 3 - Completed");
                        int statusChoice;
                        if (int.TryParse(Console.ReadLine(), out statusChoice) || statusChoice > 1 || statusChoice < 3)
                        {
                            Status status = (Status)(statusChoice - 1);

                            foreach (var project in projects.Keys.Where(p => p.Status == status))
                            {
                                Console.WriteLine($"Project: {project.Name} ({project.Status})");
                                return;
                            }
                        }
                        Console.Clear();
                        Console.WriteLine($"Invalid choice. Please try again.");
                    }
                }

                //manage projects function
                static void ManageProject()
                {
                    foreach (var oneProject in projects)
                    {
                        Console.WriteLine($"Project: {oneProject.Key.Name} ({oneProject.Key.Status})");
                    }
                    Project project = null;

                    //existing project validation
                    while (project == null)
                    {
                        string name = InputValidator.GetValidatedString("\nEnter the name of the project to manage: ");

                        project = projects.Keys.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                        if (project == null)
                        {
                            Console.WriteLine("Project not found. Please try again.");
                        }
                    }

                    Console.Clear();

                    // manage project menu
                    while (true)
                    {
                        string projectMenu = $@"
Project Management Menu for '{project.Name}':
1 Display all tasks
2 Display project details 
3 Update project status
4 Add task
5 Delete task from project
6 Show total duration of active tasks
7 Sort task ( from shortest to longest )
0 Back to main menu
";
                        Console.WriteLine(projectMenu);

                        Console.Write("Select an option: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "0":
                                Console.Clear();
                                Console.WriteLine("Exiting manage project menu...");
                                return;
                            case "1":
                                Console.Clear();
                                DisplayAllTasks(project);
                                break;
                            case "2":
                                Console.Clear();
                                DisplayProjectDetails(project);
                                break;
                            case "3":
                                Console.Clear();
                                UpdateProjectStatus(project);
                                break;
                            case "4":
                                Console.Clear();
                                AddTask(project);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid option, please try again.");
                                break;
                        }

                        //function to display all tasks of the projects
                        static void DisplayAllTasks(Project project)
                        {
                            Console.WriteLine($"\nTasks for project '{project.Name}':");
                            foreach (var task in projects[project])
                            {
                                Console.WriteLine($"- Task: {task.Name} (Status: {task.Status}, Deadline: {task.Deadline.ToShortDateString()})");
                            }
                        }

                        //function to show project details
                        static void DisplayProjectDetails(Project project)
                        {
                            Console.WriteLine($@"
Project Details for '{project.Name}':
Description: {project.Description}
Start Date: {project.StartDate.ToShortDateString()}
End Date: {project.EndDate.ToShortDateString()}
Status: {project.Status}
");
                        }

                        // function to change project status
                        static void UpdateProjectStatus(Project project)
                        {
                            if (InputValidator.IsProjectCompleted(project, projects))
                            {
                                return; 
                            }

                            Console.WriteLine($@"
Current status of project '{project.Name}': {project.Status}

Select new status for the project:
1 - Active
2 - Paused
3 - Completed
");

                            while (true)
                            {
                                int statusChoice = InputValidator.GetValidatedNumber("Enter your choice: ");

                                if (statusChoice >= 1 && statusChoice <= 3)
                                {
                                    Status newStatus = (Status)(statusChoice - 1);
                                    project.Status = newStatus;

                                    if (newStatus == Status.Completed)
                                    {
                                        InputValidator.MarkAllTasksCompleted(project, projects); 
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"Project '{project.Name}' status successfully updated to '{project.Status}'.");
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. The choice must be between 1 and 3.");
                                }
                            }
                        }


                        // function to add a task to a project
                        static void AddTask(Project project)
                        {
                            if (InputValidator.IsProjectCompleted(project, projects))
                            {
                                return;
                            }

                            Console.WriteLine($"Adding a new task to project '{project.Name}'");

                            string taskName;
                            while (true)
                            {
                                taskName = InputValidator.GetValidatedString("Enter task name: ");
                                if (!projects[project].Any(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
                                {
                                    break; 
                                }
                                Console.WriteLine($"A task with the name '{taskName}' already exists in project '{project.Name}'. Cannot add duplicate tasks.");
                            }

                            string taskDescription = InputValidator.GetValidatedString("Enter task description: ");

                            DateTime taskDeadline;
                            while (true)
                            {
                                taskDeadline = InputValidator.GetValidatedDate("Enter task deadline (yyyy-MM-dd): ");
                                if (taskDeadline >= project.StartDate && taskDeadline <= project.EndDate)
                                {
                                    break; 
                                }
                                Console.WriteLine($"Invalid deadline. The task deadline must be between the project's start date ({project.StartDate:yyyy-MM-dd}) and end date ({project.EndDate:yyyy-MM-dd}).");
                            }

                            Status taskStatus;
                            while (true)
                            {
                                taskStatus = InputValidator.GetValidatedStatus("Enter task status (Active, Paused, Completed): ");
                                if (Status.IsDefined(typeof(Status), taskStatus))
                                {
                                    break; 
                                }
                                Console.WriteLine("Invalid status. Please enter one of the following: Active, Paused, Completed.");
                            }

                            int taskDuration;
                            while (true)
                            {
                                taskDuration = InputValidator.GetValidatedNumber("Enter task duration (in minutes): ");
                                if (taskDuration > 0)
                                {
                                    break; 
                                }
                                Console.WriteLine("Invalid duration. Please enter a positive number.");
                            }

                            ProjectTask newTask = new ProjectTask(taskName, taskDescription, taskDeadline, taskStatus, taskDuration, project);
                            projects[project].Add(newTask);

                            Console.Clear();
                            Console.WriteLine($"Task '{taskName}' has been successfully added to project '{project.Name}'!");
                        }

                    }
                }

            }
        }
    }
}
