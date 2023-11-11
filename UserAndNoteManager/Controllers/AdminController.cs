using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;
using UserAndNoteManager.MyHub;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static List<string>? Messages = new List<string>();

        private IHubContext<HubContext> _hubContext;
        public AdminController(IHubContext<HubContext> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Method for Send A Message As An Admin
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("AdminMessage")]
        public async Task<JsonResult> AdminMessage([FromBody] JsonElement body)
        {
            if (body.TryGetProperty("Message", out JsonElement propertyValue))
            {
                string Message = body.GetProperty("Message").GetString();
                Messages.Add(Message);

                if (Messages.Count == 10)
                {
                    await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", "Message Counts Are 10 Now");

                    string Messagestr = string.Empty;
                    foreach (string str in Messages)
                    {
                        Messagestr = Messagestr + " " + str + " ";
                    }

                    await _hubContext.Clients.All.SendAsync("SendAdminMessage", Messagestr);

                    Messages.Clear();
                    await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", "Message Counts Are 0 Now");
                }
                return Common.OkResult();
            }
            return Common.BadRequest("Message Is Not Included");
        }

    }
}
