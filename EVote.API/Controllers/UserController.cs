using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public class UserController : Controller
    {
        private readonly UserService _service;

        public UserController()
        {
            _service = new UserService();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string SQLDatabaseName, [FromBody] UserModel model)
        {
            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await _service.CompareAsync(context, model);
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
                    Message = "Critical failure while moving Users",
                    Success = false
                });
            }
        }
    }
}
