using GraduationProjectBMS.Repositories.MLModel;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private readonly IMLModel _mlModel;

    public ChatHub(IMLModel mlModel)
    {
        _mlModel = mlModel;
    }

    public async Task SendMessage(string user, string message)
    {
        if (_mlModel.IsMessageAllowed(message))
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Your message contains inappropriate content.");
        }
    }
}
