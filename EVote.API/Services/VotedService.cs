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
    public class VotedService
    {
        public async Task<ResponseViewModel<VoterActivity>> CreateAsync(string connection, VoterDataModel model)
        {
            try
            {
                

                var context = new ElectionContext(connection);
                if (context == null)
                    throw new Exception();

                if (model == null)
                    throw new Exception();

                //var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Mountain Standard Time");

                DateTime? DateIssued = null;
                if (model.DateIssued != null) DateIssued = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.DateIssued.Value, "Mountain Standard Time");

                DateTime? PrintedDate = null;
                if (model.PrintedDate != null) PrintedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.PrintedDate.Value, "Mountain Standard Time");

                DateTime? DateVoted = null;
                if (model.DateVoted != null) DateVoted = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.DateVoted.Value, "Mountain Standard Time");

                //DateTime? ActivityDate = null;
                //if (model.ActivityDate != null) PrintedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time");

                VoterActivity voted = new VoterActivity()
                {
                    VoterId = model.VoterID,
                    //DateIssued = model.DateIssued,
                    DateIssued = DateIssued,
                    //PrintedDate = model.PrintedDate,
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
                    //ActivityDate = model.ActivityDate,
                    ActivityDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    //LastModified = model.ActivityDate.Value,
                    LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    BarCode = model.BarCode,
                    ComputerName = model.ComputerName
                };                

                context.VoterActivities.Add(voted);
                await context.SaveChangesAsync();

                return new ResponseViewModel<VoterActivity>
                {
                    Success = true,
                    Message = "Voted record created.",
                    //Result = _mapper.Map<VotedViewModel>(voted)
                    Result = voted
                };
            }
            catch (Exception e)
            {
                var rtn = new ResponseViewModel<VoterActivity>
                {
                    Success = false,
                    Result = new VoterActivity(),
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<VoterActivity>> UpdateAsync(string connection, VoterDataModel model)
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
            if (model.DateVoted != null) DateVoted = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.DateVoted.Value, "Mountain Standard Time");


            var voted = await context.VoterActivities.FindAsync(model.VoterID);
            if(voted != null)
            {
                voted.VoterId = model.VoterID;
                //voted.DateIssued = model.DateIssued;
                voted.DateIssued = DateIssued;
                //voted.PrintedDate = model.PrintedDate;
                voted.PrintedDate = PrintedDate;
                voted.AddressLine1 = model.DeliveryAddress1;
                voted.AddressLine2 = model.DeliveryAddress2;
                voted.City = model.DeliveryCity;
                voted.State = model.DeliveryState;
                voted.Zip = model.DeliveryZip;
                voted.Country = model.DeliveryCountry;
                voted.LocationId = model.LocationID;
                //voted.DateVoted = model.DateVoted;
                voted.DateVoted = DateVoted;
                voted.JurisdictionId = model.JurisdictionID;
                voted.BallotStyleId = model.BallotStyleID;
                voted.BallotNumber = model.BallotNumber;
                voted.StatusId = model.LogCode;
                //voted.ActivityDate = model.ActivityDate;
                voted.ActivityDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time");
                //voted.LastModified = model.ActivityDate.Value;
                voted.LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time");
                voted.BarCode = model.BarCode;
                voted.ComputerName = model.ComputerName;
            }

            await context.SaveChangesAsync();

            return new ResponseViewModel<VoterActivity>
            {
                Success = true,
                Message = "Voted record created.",
                //Result = _mapper.Map<VotedViewModel>(voted)
                Result = voted
            };
        }
    }
}
