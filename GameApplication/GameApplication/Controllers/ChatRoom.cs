using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GameApplication.Controllers
{
    public class ChatRoom : Hub
    {
        public async Task Send(string nick, string message)
        {
            await Clients.All.InvokeAsync("Send", nick, message);
        }
    }
}