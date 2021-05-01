using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using NewsAPI.Models;
using Server.Handlers;


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
        [HttpGet]
        public List<Article> Get()
        {
            
            return handler_.search();
        }
    }
}
