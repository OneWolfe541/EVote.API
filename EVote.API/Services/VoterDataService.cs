using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class VoterDataService
    {
        private IQueryable<VoterDataModel> Query(ElectionContext context)
        {
            var query = from voters in context.Voters
                        join votedrecord in context.VoterActivities
                            on voters.VoterId equals votedrecord.VoterId into votedrecordgroup
                        from votedRecord in votedrecordgroup.DefaultIfEmpty()
                        join status in context.Statuses
                            on votedRecord.StatusId equals status.StatusId into votedstatusgroup
                        from votedStatus in votedstatusgroup.DefaultIfEmpty()
                        join location in context.Locations
                            on votedRecord.LocationId equals location.LocationId into votedlocationgroup
                        from votedLocation in votedlocationgroup.DefaultIfEmpty()
                        join jurisdictions in context.Jurisdictions
                            on voters.JurisdictionId equals jurisdictions.JurisdictionId into voterjurisdictionsgroup
                        from voterJurisdiction in voterjurisdictionsgroup.DefaultIfEmpty()
                        join ballotjurisdictions in context.BallotStyleJurisdictions
                            on voters.JurisdictionId equals ballotjurisdictions.JurisdictionId into voterballotjurisdictionsgroup
                        from voterBallotJurisdiction in voterballotjurisdictionsgroup.DefaultIfEmpty()
                        join ballotStyles in context.BallotStyles
                            on voterBallotJurisdiction.BallotStyleId equals ballotStyles.BallotStyleId into voterballotgroup
                        from voterBallot in voterballotgroup.DefaultIfEmpty()
                        select new
                        {
                            voters.VoterId,
                            voters.FirstName,
                            voters.MiddleName,
                            voters.LastName,
                            voters.Suffix,
                            voters.MaidenName,
                            voters.DateOfBirth,
                            voters.Gender,
                            voters.Phone,
                            voters.JurisdictionId,
                            voters.InvalidMailingAddress,
                            voters.InvalidRegisteredAddress,
                            voters.MailingAddress,
                            voters.MailingAddress2,
                            voters.MailingCity,
                            voters.MailingState,
                            voters.MailingZip,
                            voters.MailingCountry,
                            voters.RegisteredAddress,
                            voters.RegisteredAddress2,
                            voters.RegisteredCity,
                            voters.RegisteredState,
                            voters.RegisteredZip,
                            voters.RegisteredCountry,
                            voters.TempAddress,
                            voters.TempAddress2,
                            voters.TempCity,
                            voters.TempState,
                            voters.TempZip,
                            voters.TempCountry,
                            voters.OnReservation,
                            voters.Registered,
                            voters.LastModified,
                            voters.SignatureVerificationId,
                            voters.UseTempAddress,
                            DeliveryAddress1 = votedRecord.AddressLine1,
                            DeliveryAddress2 = votedRecord.AddressLine2,
                            DeliveryCity = votedRecord.City,
                            DeliveryState = votedRecord.State,
                            DeliveryZip = votedRecord.Zip,
                            votedRecord.StatusId,
                            DeliveryCountry = votedRecord.Country,
                            votedRecord.BarCode,
                            votedRecord.BallotNumber,
                            votedStatus.StatusDescription,
                            votedRecord.LocationId,
                            votedLocation.LocationName,
                            votedRecord.DateIssued,
                            votedRecord.PrintedDate,
                            votedRecord.DateVoted,
                            votedRecord.ActivityDate,
                            voterJurisdiction.JurisdictionName,
                            voterJurisdiction.JurisdictionType,
                            voterBallotJurisdiction.BallotStyleId,
                            voterBallot.BallotStyleName,
                            voterBallot.BallotStyleFileName
                        };

            return query
                .Select(v => new VoterDataModel
                {
                    VoterID = v.VoterId,
                    LastName = v.LastName.ToUpper(),
                    FirstName = v.FirstName.ToUpper(),
                    MiddleName = v.MiddleName.ToUpper(),
                    Generation = v.Suffix.ToUpper(),
                    DOB = v.DateOfBirth,
                    DOBSearch = v.DateOfBirth.ToString(),
                    MaidenName = v.MaidenName.ToUpper(),
                    FullName = String.Concat(
                        v.FirstName.ToUpper(), " ",
                        v.MiddleName.ToUpper(), " ",
                        v.LastName.ToUpper(), " ",
                        v.Suffix.ToUpper())
                        .Trim().Replace("  ", " "),
                    SirnameOrdered = String.Concat(
                        v.LastName.ToUpper(), ", ",
                        v.FirstName.ToUpper(), " ",
                        v.MiddleName.ToUpper(), " ",
                        v.Suffix.ToUpper())
                        .Trim().Replace("  ", " "),
                    Gender = v.Gender,
                    Phone = v.Phone,
                    InvalidMailingAddress = v.InvalidMailingAddress,
                    InvalidRegisteredAddress = v.InvalidRegisteredAddress,
                    MailingAddress1 = v.MailingAddress.ToUpper(),
                    MailingAddress2 = v.MailingAddress2.ToUpper(),
                    MailingCity = v.MailingCity.ToUpper(),
                    MailingState = v.MailingState.ToUpper(),
                    MailingZip = v.MailingZip.ToUpper(),
                    MailingCountry = v.MailingCountry.ToUpper(),
                    PhysicalAddress1 = String.Concat(
                        v.RegisteredAddress.ToUpper(), " ",
                        v.RegisteredAddress2.ToUpper()),
                    //PhysicalAddress1 = v.RegisteredAddress.ToUpper(),
                    //PhysicalAddress2 = v.RegisteredAddress2.ToUpper(),
                    PhysicalCity = v.RegisteredCity.ToUpper(),
                    PhysicalState = v.RegisteredState.ToUpper(),
                    PhysicalZip = v.RegisteredZip,
                    PhysicalCountry = v.RegisteredCountry.ToUpper(),
                    PhysicalCSZ = String.Concat(
                        v.RegisteredCity.ToUpper(), ", ",
                        v.RegisteredState.ToUpper(), " ",
                        v.RegisteredZip),
                    TempAddress1 = v.TempAddress.ToUpper(),
                    TempAddress2 = v.TempAddress2.ToUpper(),
                    TempCity = v.TempCity.ToUpper(),
                    TempState = v.TempState.ToUpper(),
                    TempZip = v.TempZip.ToUpper(),
                    TempCountry = v.TempCountry.ToUpper(),
                    DeliveryAddress1 = v.DeliveryAddress1.ToUpper(),
                    DeliveryAddress2 = v.DeliveryAddress2.ToUpper(),
                    DeliveryCity = v.DeliveryCity.ToUpper(),
                    DeliveryState = v.DeliveryState.ToUpper(),
                    DeliveryZip = v.DeliveryZip.ToUpper(),
                    DeliveryCountry = v.DeliveryCountry.ToUpper(),
                    OnReservation = v.OnReservation,
                    Registered = v.Registered,
                    SignatureVerificationId = v.SignatureVerificationId,
                    TempUsed = v.UseTempAddress,
                    LogCode = v.StatusId,
                    //LogDescription = v.StatusDescription.ToUpper(),
                    LogDescription = v.StatusDescription == null ? "REGISTERED TO VOTE" : v.StatusDescription.ToUpper(),
                    LocationID = v.LocationId,
                    LocationName = v.LocationName.ToUpper(),
                    BallotStyleID = v.BallotStyleId,
                    BallotNumber = v.BallotNumber,
                    BarCode = v.BarCode,
                    BallotStyleName = v.BallotStyleName,
                    BallotStyleFileName = v.BallotStyleFileName,
                    JurisdictionID = v.JurisdictionId,
                    DistrictName = v.JurisdictionName.ToUpper(),
                    DateIssued = v.DateIssued,
                    PrintedDate = v.PrintedDate,
                    DateVoted = v.DateVoted,
                    ActivityDate = v.ActivityDate,
                    QueuedForPrint = ((v.StatusId == 10 || v.StatusId == 11) && v.PrintedDate == null) ? true : false
                }
                );
        }

        public async Task<List<VoterDataModel>> SelectVoters(ElectionContext context, VoterSearchModel search)
        {
            return await Query(context)
                        .IDEquals(search.VoterID)
                        .BarCodeEquals(search.BarCode)
                        .LastNameStartsWith(search.LastName)
                        .FirstNameStartsWith(search.FirstName)
                        .MaidenNameStartsWith(search.MaidenName)
                        //.BirthDateEquals(search.SearchDate.ToString())
                        .BirthDateEqualsDate(search.SearchDate.ToString())
                        .BirthYearContains(search.BirthYear)
                        .AtPollSite(search.Location)
                        .WithLogCode(search.Status)
                        .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                        .Take(50)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<List<VoterDataModel>> SelectRoster(ElectionContext context, VoterSearchModel search)
        {
            return await Query(context)
                        .IDEquals(search.VoterID)
                        .BarCodeEquals(search.BarCode)
                        .LastNameStartsWith(search.LastName)
                        .FirstNameStartsWith(search.FirstName)
                        .MaidenNameStartsWith(search.MaidenName)
                        //.BirthDateEquals(search.SearchDate.ToString())
                        .BirthDateEqualsDate(search.SearchDate.ToString())
                        .BirthYearContains(search.BirthYear)
                        .AtPollSite(search.Location)
                        .WithLogCode(search.Status)
                        //.OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                        .OrderByDescending(o => o.DateVoted)
                        .Take(50)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
