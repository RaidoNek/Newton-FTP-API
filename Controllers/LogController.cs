using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newton_FTP_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newton_FTP_API.Operations;
using Newton_FTP_API.Repositories;

namespace Newton_FTP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private LogOperation logOperation;

        public LogController(LogOperation logOperation)
        {
            this.logOperation = logOperation;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTO.Log>>> Get([FromQuery] DAO.FilterLog filter)
        {
            List<Models.Log> l = await logOperation.GetAll(filter);
            List<DTO.Log> logs = await logOperation.MapAll(l);
            return Ok(logs);
        }

        [HttpPost]
        public async Task<ActionResult<DTO.Log>> AddLog([FromForm] DAO.Log log)
        {
            if (log.TypeId == null)
                return BadRequest("Invalid TypeId provided");
            if (log.FtpId == null)
                return BadRequest("Invalid FtpId provided");
            if (log.Success == null)
                return BadRequest("Invalid Success state provided");
            if (log.Message == null)
                return BadRequest("Invalid Message provided");

            if (log.id == null)
            {
                var LogModel = await logOperation.AddLog(log);
                return Ok(await logOperation.Map(LogModel));
            } 
            else
            {
                return Ok(await logOperation.Map(await logOperation.UpdateLog(log)));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            await logOperation.DeleteLog(id);
            return Ok();
        }
    }
}
