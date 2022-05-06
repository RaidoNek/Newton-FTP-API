using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newton_FTP_API.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FTPController : ControllerBase
    {
        private FTPOperation ftpOperation;

        public FTPController(FTPOperation ftpOperation)
        {
            this.ftpOperation = ftpOperation;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTO.FTP>>> Get([FromQuery] DAO.FilterFTP filter)
        {
            List<Models.FTP> l = await ftpOperation.GetAllFTPs(filter);
            List<DTO.FTP> dtoFTPs = ftpOperation.MapAll(l);
            return dtoFTPs;
        }

        [HttpPost]
        public async Task<ActionResult<DTO.Log>> AddFTP([FromForm] DAO.FTP ftp)
        {
            if (ftp.Id == null)
            {
                try
                {
                    Models.FTP FTPModel = await ftpOperation.AddFTP(ftp);
                    return Ok(await ftpOperation.Map(FTPModel));
                } catch(FormatException e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
            return Ok(await ftpOperation.Map(await ftpOperation.UpdateFTP(ftp)));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await ftpOperation.DeleteFTP(id);
            return Ok();
        }
    }
}
