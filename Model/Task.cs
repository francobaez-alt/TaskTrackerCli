
namespace TaskTrackerCli.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string description { get; set; } = string.Empty;
        public TaskStatus status { get; set; } = TaskStatus.ToDo;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; }
    }
}

