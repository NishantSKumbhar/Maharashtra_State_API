using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Custom_Action_Filter;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;
using PatanWalks.Repositories;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DivisionController : ControllerBase
    {
        private readonly MaharashtraDbContext maharashtraDbContext;
        private readonly IDivisionRepository divisionRepository;
        private readonly IMapper mapper;
        public DivisionController(MaharashtraDbContext maharashtraDbContex, IDivisionRepository divisionRepo, IMapper mapper)
        {
            this.maharashtraDbContext = maharashtraDbContext;
            this.divisionRepository = divisionRepo;
            this.mapper = mapper;
        }

        // For Filtering ..filterOn = column name, fileterQuery = actualt string enter by user in search box
        // both are nullable
        //api/District?fileron=Name&filterQuery="Pune"
        // now also implement sorting
        // api/District?fileron=Name&filterQuery="Pune"&sortby="Name"&isAsending=true
        // remember that sorting , filtering parameters should be nullable, so think.
        // now pagination
        // api/District?fileron=Name&filterQuery="Pune"&sortby="Name"&isAsending=true&pageNumber=1&pageSize10
        [HttpGet]
        
        public async Task<ActionResult<List<DivisionGetDTO>>> GetAllDivisions(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool isAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize=10)  // change due to async
        {
            //var DivisionDTO = new List<DivisionGetDTO>();

            //var Divisions = await maharashtraDbContext.Divisions.ToListAsync();// change due to async
            
            // we have to pass that parameter to repository.
            var Divisions = await divisionRepository.GetAllDivisionsAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            //foreach (var Division in Divisions) 
            //{
            //    DivisionDTO.Add(new DivisionGetDTO
            //    {
            //        Name= Division.Name,
            //        Code = Division.Code,
            //        DivisionImageUrl= Division.DivisionImageUrl
            //    });
            //}

            //Mapping Domain Models to DTO
            var DivisionDTO = mapper.Map<List<DivisionGetDTO>>(Divisions);
            return Ok(DivisionDTO);
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<DivisionGetDTO>> GetDivisionByID(Guid id) // change due to async
        {
            var division = await divisionRepository.GetDivisionByIdAsync(id);// change due to async

            if (division == null)
            {
                return NotFound(new { Message = "ID NOT FOUND FOR PARTICULAR DIVISION" });
            }

            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            return Ok(divisionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult> PostDivision([FromBody] DivisionPostDTO newDivision)// change due to async
        {
            if (ModelState.IsValid)
            {
                var divisionModel = new Division
                {
                    Name = newDivision.Name,
                    Code = newDivision.Code,
                    DivisionImageUrl = newDivision.DivisionImageUrl
                };

                var division = await divisionRepository.PostDivisionAsync(divisionModel);// change due to async
                                                                                         //await maharashtraDbContext.SaveChangesAsync();// change due to async

                var divisionDTO = new DivisionGetDTO()
                {
                    Name = division.Name,
                    Code = division.Code,
                    DivisionImageUrl = division.DivisionImageUrl
                };

                return CreatedAtAction(nameof(GetDivisionByID), new { id = divisionModel.Id }, divisionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<DivisionGetDTO>> UpdateDivision([FromRoute] Guid id, [FromBody] DivisionPutDTO updatedDivision)// change due to async
        {
            if (ModelState.IsValid)
            {
                var divisionModel = new Division
                {
                    Code = updatedDivision.Code,
                    Name = updatedDivision.Name,
                    DivisionImageUrl = updatedDivision.DivisionImageUrl
                };
                var division = await divisionRepository.UpdateDivisionAsync(id, divisionModel);// change due to async
                if (division == null)
                {
                    return NotFound();
                }
                //division.Name = updatedDivision.Name;
                //division.Code = updatedDivision.Code;
                //division.DivisionImageUrl = updatedDivision.DivisionImageUrl;

                //await maharashtraDbContext.SaveChangesAsync();// change due to async

                var divisionDTO = new DivisionGetDTO()
                {
                    Name = division.Name,
                    Code = division.Code,
                    DivisionImageUrl = division.DivisionImageUrl
                };
                return Ok(divisionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DivisionGetDTO>> DeleteDivision([FromRoute] Guid id)// change due to async
        {
            var division = await divisionRepository.DeleteDivisionAsync(id);// change due to async
            if (division == null)
            {
                return NotFound();
            }
            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            //maharashtraDbContext.Divisions.Remove(division);
            //await maharashtraDbContext.SaveChangesAsync();// change due to async
            return Ok(divisionDTO);
        }

    }
}
