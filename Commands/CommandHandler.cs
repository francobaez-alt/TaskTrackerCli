using TaskTrackerCli.Service;

namespace TaskTrackerCli.Commands
{
    public class CommandHandler
    {
        private readonly TaskService _taskService;

        public CommandHandler()
        {
            _taskService = new TaskService();
        }


        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "Invalid command. Type \"ShowHelp\" to view available commands."
                );

                return;
            }


            var command = args[0].ToLower();


            switch (command)
            {
                case "showhelp":
                    ShowHelp();
                    break;


                case "add":
                    Add(args);
                    break;


                case "list":
                    List(args);
                    break;


                case "delete":
                    Delete(args);
                    break;


                case "update":
                    Update(args);
                    break;


                case "markinprogress":
                    MarkInProgress(args);
                    break;


                case "markdone":
                    MarkDone(args);
                    break;


                default:
                    Console.WriteLine(
                        $"Unknown command: {args[0]}"
                    );

                    Console.WriteLine(
                        "Type \"ShowHelp\" to view available commands."
                    );

                    break;
            }
        }


        private void Add(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(
                    "Description is required. Use: Add \"description\""
                );

                return;
            }


            var response = _taskService.Add(args[1]);

            Console.WriteLine(response.Message);
        }


        private void List(string[] args)
        {
            string status = args.Length > 1
                ? args[1]
                : "";

            var response = _taskService.List(status);

            Console.WriteLine(response.Message);


            if (response.Data != null)
            {
                foreach (var task in response.Data)
                {
                    Console.WriteLine(task);
                }
            }
        }


        private void Delete(string[] args)
        {
            if (!TryGetId(args, out int id))
                return;


            var response = _taskService.Delete(id);

            Console.WriteLine(response.Message);
        }


        private void Update(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine(
                    "Usage: Update <id> \"new description\""
                );

                return;
            }


            if (!int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Id must be a number.");
                return;
            }


            var response = _taskService.Update(
                id,
                args[2]
            );


            Console.WriteLine(response.Message);
        }


        private void MarkInProgress(string[] args)
        {
            if (!TryGetId(args, out int id))
                return;


            var response = _taskService.MarkInProgress(id);

            Console.WriteLine(response.Message);
        }


        private void MarkDone(string[] args)
        {
            if (!TryGetId(args, out int id))
                return;


            var response = _taskService.MarkDone(id);

            Console.WriteLine(response.Message);
        }


        private void ShowHelp()
        {
            Console.WriteLine(
                _taskService.ShowHelp()
            );
        }


        private bool TryGetId(string[] args, out int id)
        {
            id = 0;


            if (args.Length < 2)
            {
                Console.WriteLine(
                    "Id is required."
                );

                return false;
            }


            if (!int.TryParse(args[1], out id))
            {
                Console.WriteLine(
                    "Id must be a number."
                );

                return false;
            }


            return true;
        }
    }
}