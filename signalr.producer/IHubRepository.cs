using System;
using System.Threading.Tasks;

namespace signalr.producer
{
    /// <summary>
    /// Marks a type as requiring asynchronous initialization and provides the result of that initialization.
    /// </summary>
    public interface IHubRepository
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }
        void ReceiveMessage(Action<string> function);
        Task SendMessage(string message);
    }
}
