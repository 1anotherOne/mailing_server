using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI.Models;
using Server.Handlers;
using Server.Models;
using Newtonsoft.Json.Linq;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchHandler handler_;
        public SearchController(SearchHandler handler)
        {
            handler_ = handler;
        }

        [HttpGet("get")]
        public async Task<List<Article>> Get(string query)
        {
            return await Task.Run(() => {
                return handler_.search(query);
            });
        }
    }
}
