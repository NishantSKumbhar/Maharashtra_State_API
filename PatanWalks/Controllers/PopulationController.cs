using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationController : ControllerBase
    {
        private readonly MaharashtraDbContext maharashtraDbContext;

        public PopulationController(MaharashtraDbContext maharashtraDbContext)
        {
            this.maharashtraDbContext = maharashtraDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Population>>> GetPopulation()
        {
            var ps = await maharashtraDbContext.Populations.ToListAsync();
            return ps;
        }
    }
}
