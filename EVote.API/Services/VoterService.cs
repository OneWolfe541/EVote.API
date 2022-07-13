using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Models;
using EVote.API.Models.Data;
using EVote.API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.API.Services
{
    public class VoterService
    {
        public async Task<ResponseViewModel<Voter>> CreateAsync(string SQLDatabaseName, Voter model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();                

                context.Voters.Add(model);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Voter>
                {
                    Success = true,
                    Message = "Voted record created.",
                    //Result = _mapper.Map<Voter>(voted)
                    Result = model
                };
            }
            catch
            {
                var rtn = new ResponseViewModel<Voter>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<Voter>> CreateAsync(VoterDataModel model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                Voter newVoter = new Voter()
                {
                    VoterId = model.VoterID,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Suffix = model.Generation,
                    MaidenName = model.MaidenName,
                    DateOfBirth = model.DOB,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    JurisdictionId = model.JurisdictionID,
                    InvalidRegisteredAddress = model.InvalidRegisteredAddress,
                    InvalidMailingAddress = model.InvalidMailingAddress,
                    MailingAddress = model.MailingAddress1,
                    MailingAddress2 = model.MailingAddress2,
                    MailingCity = model.MailingCity,
                    MailingState = model.MailingState,
                    MailingZip = model.MailingZip,
                    MailingCountry = model.MailingCountry,
                    RegisteredAddress = model.PhysicalAddress1,
                    RegisteredAddress2 = model.PhysicalAddress2,
                    RegisteredCity = model.PhysicalCity,
                    RegisteredState = model.PhysicalState,
                    RegisteredZip = model.PhysicalZip,
                    RegisteredCountry = model.PhysicalCountry,
                    TempAddress = model.TempAddress1,
                    TempAddress2 = model.TempAddress2,
                    TempCity = model.TempCity,
                    TempState = model.TempState,
                    TempZip = model.TempZip,
                    TempCountry = model.TempCountry,
                    OnReservation = model.OnReservation,
                    Registered = model.Registered,
                    //LastModified = model.ActivityDate.Value,
                    LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time"),
                    SignatureVerificationId = model.SignatureVerificationId??0,
                    UseTempAddress = model.TempUsed,
                    ComputerName = model.ComputerName
                };

                context.Voters.Add(newVoter);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Voter>
                {
                    Success = true,
                    Message = "Voted record created.",
                    //Result = _mapper.Map<Voter>(voted)
                    Result = newVoter
                };
            }
            catch (Exception e)
            {
                var rtn = new ResponseViewModel<Voter>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<Voter>> CreateRangeAsync(string SQLDatabaseName, List<Voter> models)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                //var voteds = _mapper.Map<List<Voted>>(models);
                //if (voteds == null)
                //    throw new Exception();

                context.Voters.AddRange(models);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Voter>
                {
                    Success = true,
                    Message = "Voted records created.",
                    //Results = _mapper.Map<List<VotedViewModel>>(voteds)
                    Results = models
                };
            }
            catch
            {
                var rtn = new ResponseViewModel<Voter>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<Voter>> UpdateAsync(string SQLDatabaseName, string id, Voter model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                var voter = await context.Voters.FirstOrDefaultAsync(x => x.VoterId == id);
                if (voter == null)
                    throw new Exception();

                voter.FirstName = model.FirstName;
                voter.MiddleName = model.MiddleName;
                voter.LastName = model.LastName;
                voter.Suffix = model.Suffix;
                voter.MaidenName = model.MaidenName;
                voter.DateOfBirth = model.DateOfBirth;
                voter.Gender = model.Gender;
                voter.Phone = model.Phone;
                voter.JurisdictionId = model.JurisdictionId;
                voter.InvalidRegisteredAddress = model.InvalidRegisteredAddress;
                voter.InvalidMailingAddress = model.InvalidMailingAddress;
                voter.MailingAddress = model.MailingAddress;
                voter.MailingAddress2 = model.MailingAddress2;
                voter.MailingCity = model.MailingCity;
                voter.MailingState = model.MailingState;
                voter.MailingZip = model.MailingZip;
                voter.MailingCountry = model.MailingCountry;
                voter.RegisteredAddress = model.RegisteredAddress;
                voter.RegisteredAddress2 = model.RegisteredAddress2;
                voter.RegisteredCity = model.RegisteredCity;
                voter.RegisteredState = model.RegisteredState;
                voter.RegisteredZip = model.RegisteredZip;
                voter.RegisteredCountry = model.RegisteredCountry;
                voter.OnReservation = model.OnReservation;
                voter.Registered = model.Registered;
                voter.LastModified = model.LastModified;
                voter.ComputerName = model.ComputerName;

                //context.Update(voter);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Voter>
                {
                    Success = true,
                    Message = "Voter record updated.",
                    //Result = _mapper.Map<VoterViewModel>(updated)
                    Result = voter
                };
            }
            catch
            {
                var rtn = new ResponseViewModel<Voter>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<Voter>> UpdateAsync(VoterDataModel model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                var voter = await context.Voters.FirstOrDefaultAsync(x => x.VoterId == model.VoterID);
                if (voter == null)
                    throw new Exception();

                voter.FirstName = model.FirstName;
                voter.MiddleName = model.MiddleName;
                voter.LastName = model.LastName;
                voter.Suffix = model.Generation;
                voter.MaidenName = model.MaidenName;
                voter.DateOfBirth = model.DOB;
                voter.Gender = model.Gender;
                voter.Phone = model.Phone;
                voter.JurisdictionId = model.JurisdictionID;
                voter.InvalidRegisteredAddress = model.InvalidRegisteredAddress;
                voter.InvalidMailingAddress = model.InvalidMailingAddress;
                voter.MailingAddress = model.MailingAddress1;
                voter.MailingAddress2 = model.MailingAddress2;
                voter.MailingCity = model.MailingCity;
                voter.MailingState = model.MailingState;
                voter.MailingZip = model.MailingZip;
                voter.MailingCountry = model.MailingCountry;
                voter.RegisteredAddress = model.PhysicalAddress1;
                voter.RegisteredAddress2 = model.PhysicalAddress2;
                voter.RegisteredCity = model.PhysicalCity;
                voter.RegisteredState = model.PhysicalState;
                voter.RegisteredZip = model.PhysicalZip;
                voter.RegisteredCountry = model.PhysicalCountry;
                voter.TempAddress = model.TempAddress1;
                voter.TempAddress2 = model.TempAddress2;
                voter.TempCity = model.TempCity;
                voter.TempState = model.TempState;
                voter.TempZip = model.TempZip;
                voter.TempCountry = model.TempCountry;
                voter.OnReservation = model.OnReservation;
                voter.Registered = model.Registered;
                //voter.LastModified = model.ActivityDate.Value;
                voter.LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.ActivityDate.Value, "Mountain Standard Time");
                voter.SignatureVerificationId = model.SignatureVerificationId??0;
                voter.UseTempAddress = model.TempUsed;

                //context.Update(voter);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Voter>
                {
                    Success = true,
                    Message = "Voter record updated.",
                    //Result = _mapper.Map<VoterViewModel>(updated)
                    Result = voter
                };
            }
            catch (Exception e)
            {
                var rtn = new ResponseViewModel<Voter>
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
