using System;
using System.Threading.Tasks;

namespace AsyncConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                // run MainAsync and check for exceptions
                // https://github.com/aspnet/Security/issues/59
                // https://stackoverflow.com/a/42578098
                return MainAsync(args).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        private static async Task<int> MainAsync(string[] args)
        {
            Print("Runing parallel tasks");

            var task1 = Task.Run(async () => {
                await Task.Delay(5000);
                Print("Task 1 finished! (~5s)");
            });

            var task2 = Task.Run(async () => {
                await Task.Delay(3000);
                Print("Task 2 finished! (~3s)");
            });
            
            // sync wait
            Task.WaitAll(task1, task2);
            Print("All tasks are completed!");

            Print("Wait extra 2 seconds...");
            await Task.Delay(2000);

            Print("Another second...");
            await Task.Delay(1000);

            Print("Done!");
            return 0;
        }

        private static void Print(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
        }
    }
}
