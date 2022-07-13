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

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]

    public class SpoiledController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(string SQLDatabaseName, Guid id)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await context.Spoileds.Where(c => c.SpoiledBallotId == id).AsNoTracking().ToListAsync();
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
                    Message = "Critical failure while moving Spoileds",
                    Success = false
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string SQLDatabaseName, [FromBody] Spoiled model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                Spoiled newSpoiled = new Spoiled()
                {
                    SpoiledBallotId = model.SpoiledBallotId,
                    SpoiledReason = model.SpoiledReason,
                    LocationId = model.LocationId,
                    BallotStyleId = model.BallotStyleId,
                    VoterId = model.VoterId,
                    ComputerName = model.ComputerName,
                    PrintedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.PrintedDate.Value, "Mountain Standard Time"),
                    LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.LastModified, "Mountain Standard Time")
                };

                context.Spoileds.Add(newSpoiled);
                await context.SaveChangesAsync();

                var rtn = new ResponseViewModel<Spoiled>
                {
                    Success = true,
                    Message = "Spoiled records created.",
                    //Results = _mapper.Map<List<VotedViewModel>>(voteds)
                    Result = model
                };

                return Ok(rtn);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
