using BlazorApp1.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp1.Services;

public class ChatHubService : BackgroundService
{
    private readonly IHubContext<ChatHub> _context;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(2));

    public ChatHubService(IHubContext<ChatHub> context)
    {
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(await _timer.WaitForNextTickAsync(stoppingToken)
              && !stoppingToken.IsCancellationRequested)
        {
            if (_context is not null)
                await _context.Clients.All.SendAsync("ReceiveMessage", $"I am sending a message every 2 seconds!");
        }
    }
}
