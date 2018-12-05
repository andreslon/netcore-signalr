using System;
using System.Threading.Tasks;
namespace signalr.producer
{
    class Program
    {
        static IHubRepository HubRepository;
        static void Main(string[] args)
        {
            HubRepository = new HubRepository("https://yisus-signalr-app.azurewebsites.net/chatHub");
            Task.Run(async () =>
            {
                try
                {
                    await HubRepository.Initialization;
                    HubRepository.ReceiveMessage(receive);
                    await generateEventsAsync();
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                }
               
            });

            Console.ReadLine();
        }

        private static void receive(string message)
        {
            Console.WriteLine(message);
        }

        private static async Task generateEventsAsync()
        {
            for (int i = 0; i < 60000; i++)
            {
                var data = DateTime.Now.ToString();
                await HubRepository.SendMessage(data);
            }
            // Parallel.ForEach(list, str =>
            //{
            //    await connection.InvokeAsync("SendMessage", str);
            //});
        }
    }
}
