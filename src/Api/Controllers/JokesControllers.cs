using Api.data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokesContext JokeContext;
        public JokesController(IJokesContext context)
        {
            JokeContext = context;
        }
        
        [HttpGet()]
        [Route("categories")]
        public async Task<List<string>> GetCategories() => await JokeContext.GetCategories();

        [HttpGet()]
        [Route("search")]
        public async Task<Search> GetSearch(string query) => await JokeContext.GetJokes(query);
    }
}