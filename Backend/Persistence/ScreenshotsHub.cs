namespace Persistence;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ScreenshotHub : Hub
{
    public async Task SendScreenshot(string base64Image)
    {
        await Clients.All.SendAsync("NewScreenshot", base64Image);
    }
}

