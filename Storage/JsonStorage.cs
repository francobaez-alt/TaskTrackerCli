using System.Text.Json;
using TaskTrackerCli.Model;

namespace TaskTrackerCli.Storage
{
    public class JsonStorage
    {
        private const string FilePath = "Tasks.json";

        public JsonStorage()
        {
            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath, "[]");
        }

        public List<TaskItem> GetTasks()
        {
            string json = File.ReadAllText(FilePath);

            if(json == null)
                return new List<TaskItem>();

            #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return JsonSerializer.Deserialize<List<TaskItem>>(json);
            #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public void SaveTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
              
        }
    }


}
