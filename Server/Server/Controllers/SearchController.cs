using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI.Models;
using Server.Helpers;
using Server.Models;
using Newtonsoft.Json.Linq;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchHelper _searchHelper;
        private readonly EmailSenderHelper _emailSenderHelper;
        public SearchController(SearchHelper searchHelper, EmailSenderHelper emailSenderHelper)
        {
            _searchHelper = searchHelper;
            _emailSenderHelper = emailSenderHelper;
        }

        [HttpGet("get")]
        public async Task<List<Article>> Get(string query, string date=null)
        {
            return await Task.Run(() => {
                return _searchHelper.Search(query, date);
            });
        }

        [HttpGet("getViaEmail")]
        public async Task<IActionResult> GetViaEmail(string query, string destination, string date=null)
        {
            return await Task.Run<IActionResult>(() =>
            {
                List<Article> articles = _searchHelper.Search(query, date);
                _emailSenderHelper.SendEmail(query, destination, articles);
                return Ok();
            });
        }
    }
}
