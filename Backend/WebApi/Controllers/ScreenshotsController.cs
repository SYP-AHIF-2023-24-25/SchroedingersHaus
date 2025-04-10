namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IO;
using System.Threading.Tasks;
using Persistence;

[Route("api/[controller]")]
[ApiController]
public class ScreenshotsController : ControllerBase
{
    // SignalR hub context injected for broadcasting updates
    private readonly IHubContext<ScreenshotHub> _hubContext;

    public ScreenshotsController(IHubContext<ScreenshotHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> UploadScreenshot([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Read file data into memory without saving to a database.
        byte[] imageData;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();
        }

        // Convert the binary data to a base64 string for easy transmission.
        string base64Image = System.Convert.ToBase64String(imageData);

        // Immediately send it to the frontend via SignalR.
        await _hubContext.Clients.All.SendAsync("NewScreenshot", base64Image);

        return Ok("Screenshot received and forwarded.");
    }
}

