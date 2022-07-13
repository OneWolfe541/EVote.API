using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Helpers;
using EVote.API.ViewModels;
using EVote.API.Models;
using EVote.API.Services;

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]

    public class VotedController : Controller
    {
        private readonly VotedService _service;
        private readonly VotedHistoryService _history;

        public VotedController()
        {
            _service = new VotedService();
            _history = new VotedHistoryService();
        }

        public IActionResult Get()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VoterDataModel model)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var voted = await context.VoterActivities.FindAsync(model.VoterID);
                    if (voted == null)
                    {
                        var rtn = await _service.CreateAsync(connectionString, model);
                        if (rtn == null)
                        {
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                        }
                        else
                        {
                            var hist = await _history.CreateAsync(connectionString, model);
                        }
                        return Ok(rtn);
                    }
                    else
                    {
                        var rtn = await _service.UpdateAsync(connectionString, model);
                        if (rtn == null)
                        {
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                        }
                        else
                        {
                            var hist = await _history.CreateAsync(connectionString, model);
                        }
                        return Ok(rtn);
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
