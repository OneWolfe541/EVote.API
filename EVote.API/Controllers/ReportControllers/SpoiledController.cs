using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Models;
using EVote.API.Services;
using EVote.API.Context;
using EVote.API.Helpers;
using EVote.API.ViewModels;

namespace EVote.API.Controllers.ReportControllers
{
    [ApiController]
    [Route("api/reports/[controller]")]

    public class SpoiledController : Controller
    {
        private readonly ReportService _service;

        public SpoiledController()
        {
            _service = new ReportService();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await _service.SpoiledBallotDetails(context);
                    if (rtn == null)
                        return BadRequest(ResponseHelper.ContextCreationFailedResponse());

                    return Ok(rtn);
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportParametersModel model)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    if (model.type == 1 || model.type == null)
                    {
                        var rtn = await _service.SpoiledBallotDetails(context, model);
                        if (rtn == null)
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                        return Ok(rtn);
                    }
                    else
                    {
                        return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                    }
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
