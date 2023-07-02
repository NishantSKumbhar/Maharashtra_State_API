using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilesController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult GetMobiles()
        //{
            
        //    List<string> MobileNames = new List<string>();
        //    MobileNames.Add("Redmi Note 10 Pro");
        //    MobileNames.Add("Redmi Note 9 Pro");
        //    MobileNames.Add("Redmi Note 8 Pro");
        //    MobileNames.Add("Redmi Note 7 Pro");
        //    MobileNames.Add("Redmi Note 6 Pro");
        //    return Ok(MobileNames);
        //}

        [HttpGet]
        public IActionResult GetPrices()
        {
            List<int> Prices  = new List<int>();
            Prices.Add(19000);
            Prices.Add(18012);
            Prices.Add(17000);
            Prices.Add(17000);
            Prices.Add(15000);
            return Ok(Prices);
        }

    }
    

}
