using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Models.Data;
using EVote.API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.API.Services
{
    public class SignatureService
    {
        public async Task<ResponseViewModel<Signature>> CreateAsync(string SQLDatabaseName, Signature model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                if (model == null)
                    throw new Exception();

                Signature voted = new Signature()
                {
                    SignatureId = model.SignatureId,
                    VoterId = model.VoterId,
                    SignatureImage = model.SignatureImage,
                    ComputerName = model.ComputerName,
                    LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.LastModified.Value, "Mountain Standard Time")
                };

                context.Signatures.Add(voted);
                await context.SaveChangesAsync();

                return new ResponseViewModel<Signature>
                {
                    Success = true,
                    Message = "Voted record created.",
                    //Result = _mapper.Map<VotedViewModel>(voted)
                    Result = voted
                };
            }
            catch
            {
                var rtn = new ResponseViewModel<Signature>
                {
                    Success = false,
                    Error = new ModelStateDictionary()
                };
                rtn.Error.AddModelError("code", "message");

                return rtn;
            }
        }

        public async Task<ResponseViewModel<Signature>> UpdateAsync(string SQLDatabaseName, Signature model)
        {
            try
            {
                string connectionString = "XXXX";

                var context = new ElectionContext(connectionString);
                if (context == null)
                    throw new Exception();

                if (model == null)
                    throw new Exception();

                var signature = await context.Signatures.FindAsync(model.SignatureId);
                if (signature != null)
                {
                    signature.SignatureId = model.SignatureId;
                    signature.VoterId = model.VoterId;
                    signature.SignatureImage = model.SignatureImage;
                    signature.ComputerName = model.ComputerName;
                    signature.LastModified = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(model.LastModified.Value, "Mountain Standard Time");
                    
                    await context.SaveChangesAsync();

                    return new ResponseViewModel<Signature>
                    {
                        Success = true,
                        Message = "Voted record created.",
                        //Result = _mapper.Map<VotedViewModel>(voted)
                        Result = signature
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                var rtn = new ResponseViewModel<Signature>
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
