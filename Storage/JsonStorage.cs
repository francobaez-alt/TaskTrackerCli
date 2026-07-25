using System.Text.Json;

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

        public List<Task> GetTasks()
        {
            string json = File.ReadAllText(FilePath);

            if(json == null)
                return new List<Task>();

            return JsonSerializer.Deserialize<List<Task>>(json);
        }

        public void SaveTasks(List<Task> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
              
        }
    }


}
