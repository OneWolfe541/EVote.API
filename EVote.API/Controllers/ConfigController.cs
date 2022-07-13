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

    public class ConfigController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(string SQLDatabaseName, int? id)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await context.Configs.Where(c => c.LocationId == id || c.LocationId == null || id == null).AsNoTracking().ToListAsync();
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
        public async Task<IActionResult> Post(string SQLDatabaseName, [FromBody] List<Config> models)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                // Delete table
                if (models != null && models.Count() > 0)
                {
                    context.Configs.RemoveRange(context.Configs);
                }

                context.Configs.AddRange(models);
                await context.SaveChangesAsync();

                var rtn = new ResponseViewModel<Config>
                {
                    Success = true,
                    Message = "Config records created.",
                    //Results = _mapper.Map<List<VotedViewModel>>(voteds)
                    Results = models
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
