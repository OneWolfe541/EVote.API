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

    public class VoterDataController : Controller
    {
        private readonly VoterDataService _service;

        public VoterDataController()
        {
            _service = new VoterDataService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await context.Voters.FindAsync(id);
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
        public async Task<IActionResult> Post([FromBody] VoterSearchModel model, int? type)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    //var rtn = await context.Voters.FindAsync(model.VoterID);
                    //string date = model.SearchDate.ToString();
                    //var ddate = DateTime.Parse(model.SearchDate.ToString());
                    //var test = context.Voters.Where(v => v.DateOfBirth == ddate).FirstOrDefault();
                    if (type == 2)
                    {
                        var rtn = await _service.SelectRoster(context, model);
                        if (rtn == null)
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());

                        return Ok(rtn);
                    }
                    else
                    {
                        var rtn = await _service.SelectVoters(context, model);
                        if (rtn == null)
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());

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
