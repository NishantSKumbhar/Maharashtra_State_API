using Microsoft.AspNetCore.Mvc;
using PatanWalks.Models.Sample;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilesController : ControllerBase
    {

        private List<MobileModel> Mobiles = new List<MobileModel>
        {
            new MobileModel{ Id = 1, Name = "Redmi Note 10 Pro", Description = "Best in Class.", Price= 19000, Color="Black"},
            new MobileModel{ Id = 2, Name = "Apple 14 Pro", Description = "New Beginning.", Price= 90000, Color="Product Red"},
            new MobileModel{ Id = 3, Name = "One Plus 7 Pro", Description = "Never Settle.", Price= 60000, Color="Grey Silver"},
            new MobileModel{ Id = 4, Name = "Samsung S22 Ultra", Description = "Behind the Moon.", Price= 98000, Color="Glossy Red"},
            new MobileModel{ Id = 5, Name = "Sony Xperia Pro", Description = "El Classico.", Price= 70000, Color="Matfinish Black"}

        };
        // https://localhost:7104/api/Mobiles
        [HttpGet]
        public ActionResult<IEnumerable<MobileModel>> GetMobiles()
        {
            return Mobiles;
        }

        //[HttpGet]
        //public IActionResult GetPrices()
        //{
        //    List<int> Prices  = new List<int>();
        //    Prices.Add(19000);
        //    Prices.Add(18012);
        //    Prices.Add(17000);
        //    Prices.Add(17000);
        //    Prices.Add(15000);
        //    return Ok(Prices);
        //}

        // https://localhost:7104/api/Mobiles/2
        [HttpGet("{id}")]
        public ActionResult<MobileModel> Get(int id)
        {
            MobileModel mobile = Mobiles.FirstOrDefault(m => m.Id == id);
            if (mobile == null)
            {
                return NotFound(new { Messsage = "Mobile Not Found with that ID." });
            }
            return Ok(mobile);
        }

        [HttpPost]
        public ActionResult<IEnumerable<MobileModel>> Post(MobileModel mobileNew)
        {
            Mobiles.Add(mobileNew);
            return Ok(Mobiles);
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<MobileModel>> Put(int id, MobileModel mobileUpdated)
        {
            foreach(MobileModel m in Mobiles)
            {
                if(m.Id == id)
                {
                    m.Name = mobileUpdated.Name;
                    m.Price = mobileUpdated.Price;
                    m.Color = mobileUpdated.Color;
                    m.Description = mobileUpdated.Description;
                    return Ok(Mobiles);
                }
                
            }
            return NotFound(new { Message = "For the Updation Id not Matched ." });
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<MobileModel>> Delete(int id, MobileModel mobileUpdated)
        {
            foreach (MobileModel m in Mobiles)
            {
                if (m.Id == id)
                {
                    Mobiles.Remove(m);
                    return Ok(Mobiles);
                }

            }
            return NotFound(new { Message = "For the Deletion Id not Matched ." });
        }
    }


}
