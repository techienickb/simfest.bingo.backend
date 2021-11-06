using Microsoft.AspNetCore.SignalR;

namespace Simfest.Bingo.Backend
{
    public class BingoHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connection from " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Disconnection from " + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNewGame()
        {
            Console.WriteLine("New Game Called");
            await Clients.Others.SendAsync("NewGame");
        }

        public async Task IsWinner(int[] ids, string Name)
        {
            Console.WriteLine("Is Winner called");
            await Clients.Others.SendAsync("IsWinner", new IsWinner(Context.ConnectionId, Name, ids));
        }

        public async Task Winner(string Id)
        {
            Console.WriteLine("Winner");
            await Clients.Client(Id).SendAsync("Winner");
        }

        public async Task NotWinner(string Id)
        {
            Console.WriteLine("Not a Win");
            await Clients.Client(Id).SendAsync("NotWinner");
        }

    }

    internal record IsWinner(string connectionId, string Name, int[] ids)
    {
    }
}
