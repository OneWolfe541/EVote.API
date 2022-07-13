using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Helpers;
using EVote.API.ViewModels;
using EVote.API.Models.Data;
using EVote.API.Services;
using EVote.API.Models.Stats;

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]

    public class ChartController : Controller
    {
        private readonly StatsService _service;

        public ChartController()
        {
            _service = new StatsService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string SQLDatabaseName, int? id)
        {
            try
            {
                string connectionString = "XXXX";

                //List<ChartStatsModel> rtn = null;
                if (id == 1)
                {
                    var rtn = await _service.VoterActivity(connectionString);
                    if (rtn == null)
                        return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                    return Ok(rtn);
                }
                else if (id == 2)
                {
                    var rtn = await _service.ElectionActivity(connectionString);
                    if (rtn == null)
                        return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                    return Ok(rtn);
                }
                else if (id == 3)
                {
                    var rtn = await _service.VoterCounts(connectionString);
                    if (rtn == null)
                        return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                    return Ok(rtn);
                }
                else
                {
                    return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                }
            }
            catch (Exception e)
            {
                return Ok(new ServisApiResponseViewModel
                {
                    Count = 0,
                    Error = new ServisApiResponseErrorViewModel { ErrorMessage = e.Message },
                    Message = "Critical failure while moving Voters",
                    Success = false
                });
            }
        }
    }
}
