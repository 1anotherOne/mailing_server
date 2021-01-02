using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Helpers;

namespace Server.Controllers
{
    [ApiController]
    [Route("Mail/")]
    public class MailingController : ControllerBase
    {
        private readonly EmailSenderHelper _emailSenderHelper;
        public MailingController(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
        }
        [HttpPost("Send/")]
        public async Task<IActionResult> Send(string destinationAdress)
        {
            return await Task.Run<IActionResult>(()=>
            {
                try
                {
                    _emailSenderHelper.SendEmail(destinationAdress);
                    return Ok();
                } catch(Exception err)
                {
                    return new BadRequestObjectResult(err.Message);
                }
            });
        }
    }
}
