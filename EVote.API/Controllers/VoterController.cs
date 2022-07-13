using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Helpers;
using EVote.API.ViewModels;
using EVote.API.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.Entity;
using AutoMapper;
using EVote.API.Services;
using EVote.API.Models;

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class VoterController : Controller
    {
        private readonly VoterService _service;
        private readonly VoterHistoryService _history;

        public VoterController()
        {
            _service = new VoterService();
            _history = new VoterHistoryService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string SQLDatabaseName, string id)
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
        public async Task<IActionResult> Post([FromBody] VoterDataModel model)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var voted = await context.Voters.FindAsync(model.VoterID);
                    if (voted == null)
                    {
                        var rtn = await _service.CreateAsync(model);
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
                        var rtn = await _service.UpdateAsync(model);
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

        //[HttpPost]
        //public async Task<IActionResult> Post(string SQLDatabaseName, [FromBody] List<Voter> models)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var rtn = models.Count == 1 ? await _service.CreateAsync(SQLDatabaseName, models[0]) : await _service.CreateRangeAsync(SQLDatabaseName, models);
        //        if (!rtn.Success)
        //            return BadRequest(rtn.Error);

        //        return Ok(rtn);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put(string SQLDatabaseName, string id, [FromBody] Voter model)
        //{
        //    try
        //    {
        //        var test = id;
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var rtn = await _service.UpdateAsync(SQLDatabaseName, id, model);
        //        if (!rtn.Success)
        //            return BadRequest(rtn.Error);

        //        return Ok(rtn);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}        
    }
}
