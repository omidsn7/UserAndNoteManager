using Microsoft.AspNetCore.SignalR;

namespace UserAndNoteManager.MyHub;
public sealed class HubContext : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("UserConnected", $"{Context.ConnectionId} Has Joined");
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Clients.All.SendAsync("UserDisconnected", $"{Context.ConnectionId} Disconnected");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task Send(string message)
    {
        await Clients.All.SendAsync("ChangesOnUserAndNotes", message);
    }
}
