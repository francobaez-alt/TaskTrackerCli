
using TaskTrackerCli.Model;
using TaskTrackerCli.Storage;

namespace TaskTrackerCli.Service
{
    public class TaskService
    {
        private readonly JsonStorage _storage;
        public TaskService()
        {
            _storage = new JsonStorage();
        }

        public ServiceResponse<TaskItem> Add(string description)
        {
            if (string.IsNullOrEmpty(description))
                return ServiceResponse<TaskItem>.Error(
                    "Description cannot be empty."
                    );
            

            var tasks = _storage.GetTasks();

            int newId = tasks.Count() == 0
                ? 1 : tasks.Max(x => x.Id) + 1;

            var task = new TaskItem
            {
                Id = newId,
                Description = description,
            };

            tasks.Add(task);
            _storage.SaveTasks(tasks);

            return ServiceResponse<TaskItem>.Ok(
                task, 
                $"Task Added Successfully, ID: {task.Id}"
                );
        }

        public ServiceResponse<IEnumerable<TaskItem>> List(string status = "")
        {
            var tasks = _storage.GetTasks();

            if (!tasks.Any())
                return ServiceResponse<IEnumerable<TaskItem>>.Error(
                    "You don't have any task yet."
                    );

            if (string.IsNullOrEmpty(status))
            {
                return ServiceResponse<IEnumerable<TaskItem>>.Ok(
                    tasks, 
                    "Tasks return Successfully"
                    );
            }

            if (!Enum.TryParse<Model.TaskStatus>(status, true, out var taskStatus)
                || !Enum.IsDefined(typeof(Model.TaskStatus), taskStatus))
                return ServiceResponse<IEnumerable<TaskItem>>.Error(
                    $"Invalid status: {status} \nAvailable statuses: Todo, InProgress, Done"
                    );

            var filteredTasks = tasks
                .Where(t => t.Status == taskStatus)
                .ToList();

            if (!filteredTasks.Any())
                return ServiceResponse<IEnumerable<TaskItem>>.Ok(
                    filteredTasks,
                    $"You don't have any task with status {status} to list."
                    );

            return ServiceResponse<IEnumerable<TaskItem>>.Ok(
                filteredTasks, 
                "Tasks Filtered Successfully"
                );

        }

        public ServiceResponse<TaskItem> Delete(int id)
        {
            var tasks = _storage.GetTasks();

            if (!tasks.Any())
                return ServiceResponse<TaskItem>.Error(
                    "You don't have any task yet."
                    );
            

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return ServiceResponse<TaskItem>.Error(
                    $"Task with id:{id} does not exist."
                    );

            tasks.Remove(task);
            _storage.SaveTasks(tasks);
     
            return ServiceResponse<TaskItem>.Ok(
                task, 
                $"Task {task.Description} was delete."
                );

        }

        public ServiceResponse<TaskItem> Update(int id, string newDescription)
        {
            var tasks = _storage.GetTasks();

            if (!tasks.Any())
                return ServiceResponse<TaskItem>.Error(
                    "You don't have any task yet."
                    );

            if (string.IsNullOrEmpty(newDescription)) 
                return ServiceResponse<TaskItem>.Error(
                    "Description cannot be empty."
                    );

            var task = tasks.FirstOrDefault(task => task.Id == id);

            if (task == null)
                return ServiceResponse<TaskItem>.Error(
                    $"Task with ID: {id} does not exist."
                    );

            task.Description = newDescription;

            _storage.SaveTasks(tasks);
            return ServiceResponse<TaskItem>.Ok(
                task, 
                $"Task with ID: {id} updated Successfully"
                );
        }

        public ServiceResponse<TaskItem> MarkInProgress(int id)
        {
            return UpdateStatus(id, Model.TaskStatus.InProgress);
        }


        public ServiceResponse<TaskItem> MarkDone(int id)
        {
            return UpdateStatus(id, Model.TaskStatus.Done);
        }

        public string ShowHelp()
        {
            return """
            Available commands:

            # Add a new task
            task-cli Add "Buy groceries"

            # Update and delete tasks
            task-cli Update 1 "Buy groceries and cook dinner"
            task-cli Delete 1

            # Mark a task as in progress or done
            task-cli MarkInProgress 1
            task-cli MarkDone 1

            # List all tasks
            task-cli List

            # List tasks by status
            task-cli List Done
            task-cli List Todo
            task-cli List InProgress

            # Display available commands
            task-cli ShowHelp
            """;
        }

        private ServiceResponse<TaskItem> UpdateStatus(
            int id, 
            Model.TaskStatus status)
        {
            var tasks = _storage.GetTasks();

            if (!tasks.Any())
            {
                return ServiceResponse<TaskItem>.Error(
                    "You don't have any task yet."
                );
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return ServiceResponse<TaskItem>.Error(
                    $"Task with ID: {id} does not exist."
                );
            }

            task.Status = status;

            _storage.SaveTasks(tasks);

            return ServiceResponse<TaskItem>.Ok(
                task,
                $"Task ID: {id} marked as {status} successfully."
            );
        }
    }
}
