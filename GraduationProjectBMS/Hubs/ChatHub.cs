using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly BadWordsFilterService _badWordsFilter;

    public ChatHub(BadWordsFilterService badWordsFilter)
    {
        _badWordsFilter = badWordsFilter;
    }

    public async Task SendMessage(string user, string message)
    {
        if (_badWordsFilter.ContainsBadWords(message))
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Your message contains inappropriate content.");
        }
        else
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
