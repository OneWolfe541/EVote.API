using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Extensions;
using EVote.API.Models;
using EVote.API.Models.Data;
using EVote.API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.API.Services
{
    public class VotedHistoryService
    {
        public async Task<ResponseViewModel<VoterActivityHistory>> CreateAsync(string connection, VoterDataModel model)
        {
            try
            {
                

                var context = new ElectionContext(connection);
                if (context == null)
                    throw new Exception();

                if (model == null)
                    throw new Exception();

                DateTime? DateIssued = null;
                if (model.DateIssued != null) DateIssued = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.DateIssued.Value, "Mountain Standard Time");

                DateTime? PrintedDate = null;
                if (model.PrintedDate != null) PrintedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.PrintedDate.Value, "Mountain Standard Time");

                DateTime? DateVoted = null;
                if (model.DateVoted != null) PrintedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.DateVoted.Value, "Mountain Standard Time");


                VoterActivityHistory voted = new VoterActivityHistory()
                {
                    VoterActivityHistoryId = Guid.NewGuid(),
                    HistoryAction = model.CodeGroupState,
                    HistoryDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    HistoryDateServerUTC = DateTime.UtcNow,
                    VoterId = model.VoterID,
                    DateIssued = DateIssued,
                    PrintedDate = PrintedDate,
                    AddressLine1 = model.DeliveryAddress1,
                    AddressLine2 = model.DeliveryAddress2,
                    City = model.DeliveryCity,
                    State = model.DeliveryState,
                    Zip = model.DeliveryZip,
                    Country = model.DeliveryCountry,
                    LocationId = model.LocationID,
                    //DateVoted = model.DateVoted,
                    DateVoted = DateVoted,
                    JurisdictionId = model.JurisdictionID,
                    BallotStyleId = model.BallotStyleID,
                    BallotNumber = model.BallotNumber,
                    StatusId = model.LogCode,
                    ActivityDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    BarCode = model.BarCode,
                    ComputerName = model.ComputerName
                };

                context.VoterActivityHistories.Add(voted);
                await context.SaveChangesAsync();

                return new ResponseViewModel<VoterActivityHistory>
                {
                    Success = true,
                    Message = "Voted history record created.",
                    //Result = _mapper.Map<VotedViewModel>(voted)
                    Result = voted
                };
            }
            catch (Exception e)
            {
                var rtn = new ResponseViewModel<VoterActivityHistory>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }
    }
}
