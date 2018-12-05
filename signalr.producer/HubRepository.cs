using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace signalr.producer
{
    public class HubRepository : IHubRepository
    {
        public Task Initialization { get; private set; }
        private HubConnection Connection { get; set; }
        public string ConnectionUri { get; set; }
        public HubRepository(string connectionUri)
        {
            ConnectionUri = connectionUri;
            Initialization = InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            try
            {
                if (Connection != null)
                    return;
                Connection = new HubConnectionBuilder()
                               .WithUrl(ConnectionUri)
                               .Build();
                await Connection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ReceiveMessage(Action<string> function)
        {
            try
            {
                Connection.On("ReceiveMessage", function);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        async public Task SendMessage(string message)
        {
            try
            {
                await Connection.InvokeAsync("SendMessage", message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}
