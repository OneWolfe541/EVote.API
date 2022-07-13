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

    public class SummaryController : Controller
    {
        private readonly ReportService _service;

        public SummaryController()
        {
            _service = new ReportService();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok(await Task.Run(() =>
            //{
            //    List<DailyDetailData> data = new List<DailyDetailData>();

            //    DailyDetailData record = new DailyDetailData();
            //    record.election_id = 22;
            //    record.county_code = "";
            //    record.voter_id = 0;
            //    record.computer = 0;
            //    record.poll_id = 1;
            //    record.ballot_style_name = "";
            //    record.ballot_number = 0;
            //    record.date_voted = DateTime.MinValue;
            //    record.ballot_style_id = 0;
            //    record.sign_refused = false;
            //    record.local_only = false;
            //    record.user_id = 0;
            //    record.log_code = 10;
            //    record.acivity_code = "";
            //    record.activity_date = DateTime.MinValue;
            //    record.voter_status = "";
            //    record.name_title = "";
            //    record.name_first = "";
            //    record.name_middle = "";
            //    record.name_last = "";
            //    record.name_suffix = "";
            //    record.dob = "";
            //    record.nonStdAddressFlag = false;
            //    record.nonStdAddressDesc = "";
            //    record.place_name = "";
            //    record.election_type = "PRIMARY";
            //    record.election_name = "PRIMARY ELECTION";
            //    record.election_date = DateTime.Parse("07/01/2019");
            //    record.election_date_long = "Monday, July 1, 2019";
            //    record.log_description = "Issued Ballot";
            //    record.report_type = "Activity to Date";
            //    record.assigned_ballot_style_id = "";
            //    record.name_maiden = "";
            //    record.street_address = "";
            //    record.street_address_2 = "";
            //    record.street_city = "";
            //    record.street_state = "";
            //    record.street_zip = "";
            //    record.records_logged = 6;
            //    record.organization_name = "MUSCOGEE (CREEK) NATION";

            //    data.Add(record);

            //    return data;
            //}
            //)
            //    );

            try
            {
                string connectionString = "XXXX";

                using (var context = new ElectionContext(connectionString))
                {
                    var rtn = await _service.DailySummaryBySite(context);
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
                    if (model.type == 1)
                    {
                        var rtn = await _service.DailySummaryBySite(context, model);
                        if (rtn == null)
                            return BadRequest(ResponseHelper.ContextCreationFailedResponse());
                        return Ok(rtn);
                    }
                    else if(model.type == 2)
                    {
                        var rtn = await _service.DailySummaryByDistrict(context, model);
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
