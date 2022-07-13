using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.ViewModels;
using EVote.API.Helpers;
using EVote.API.Services;

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]

    public class AggregateController : Controller
    {
        private readonly StatsService _service;

        public AggregateController()
        {
            _service = new StatsService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string SQLDatabaseName, int? id)
        {
            try
            {
                string connectionString = "XXXX";

                if (id == 1)
                {
                    var rtn = await _service.MaxBarCode(connectionString);
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
