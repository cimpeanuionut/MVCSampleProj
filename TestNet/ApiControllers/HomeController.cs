using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using TestNet.Logic;

namespace TestNet.ApiControllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService svcHome;

        public HomeController(IHomeService svcHome)
        {
            this.svcHome = svcHome;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetFiles()
        {
            try
            {

                var ret = await svcHome.GetFilesAsync();

                return Ok(ret);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message + " " + e.InnerException);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            try
            {
                if (file == null)
                    throw new Exception("No file was submited");          
             
                await svcHome.UploadFile(file);

                return Ok("Your file has been uploaded");
            }
            catch (Exception e)
            {
                try
                {
                    await svcHome.SendError();

                    return StatusCode((int)HttpStatusCode.InternalServerError, e.Message + " " + e.InnerException);
                }
                catch (Exception ex)
                {
                    return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message +" " + ex.InnerException);
                }
            }
        }
    }
}
