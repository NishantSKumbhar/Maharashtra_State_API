using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly MaharashtraDbContext dbContext;
        public DistrictController(MaharashtraDbContext dbContext)
        {
             this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<District>> GetDistricts()
        {
            var districts = dbContext.Districts.ToList();
            return Ok(districts);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<District> GetDistrictById([FromRoute]Guid id)
        {
            //var district = dbContext.Districts.Find(id);
            // Above Find Method will take only primary key
            var district = dbContext.Districts.FirstOrDefault(x => x.Id == id);

            if (district == null) 
            {
                return NotFound(new { Message = "District with that ID not Found."});
            }
            return Ok(district);
        }
    }
}
