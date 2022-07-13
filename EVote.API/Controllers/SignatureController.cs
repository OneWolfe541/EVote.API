using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Models;
using EVote.API.Context;
using EVote.API.Helpers;
using EVote.API.ViewModels;
using EVote.API.Services;
using EVote.API.Models.Data;

namespace EVote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]

    public class SignatureController : Controller
    {
        private readonly SignatureService _service;

        public SignatureController()
        {
            _service = new SignatureService();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string SQLDatabaseName, [FromBody] Signature model)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var signature = await context.Signatures.FindAsync(model.SignatureId);
                    if (signature == null)
                    {
                        var rtn = await _service.CreateAsync(SQLDatabaseName, model);
                        if (rtn == null)
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                        return Ok(rtn);
                    }
                    else
                    {
                        var rtn = await _service.UpdateAsync(SQLDatabaseName, model);
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
                    Message = "Critical failure while moving Signatures",
                    Success = false
                });
            }
        }
    }
}
