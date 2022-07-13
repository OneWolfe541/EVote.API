using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.API.Helpers
{
    public class ResponseHelper
    {
        public static ResponseViewModel SyncUploadSuccessResponse(int added, int updated, int skipped, string entity, DateTime now, List<Guid> syncIds, List<int> voterIds)
        {
            var total = added + updated + skipped;
            var rtn = new ResponseViewModel
            {
                Message = String.Format("Successfully Uploaded {0} {1}(s). Added: {2} Updated: {3} Skipped: {4} ", total, entity, added, updated, skipped),
                Error = null,
                Success = true,
                Data = new ViewModels.Data
                {
                    Added = added,
                    Updated = updated,
                    Skipped = skipped,
                    Total = total,
                    SyncDate = now,
                    SyncIds = syncIds,
                    VoterIds = voterIds
                }
            };
            return rtn;
        }

        public static ResponseViewModel SyncUploadFailResponse(Exception e, string entity)
        {
            var rtn = new ResponseViewModel
            {
                Message = string.Format("Failed To Upload {0}(s) See Error For Details", entity),
                Success = false,
                Error = new ModelStateDictionary()
            };
            rtn.Error.AddModelError("code", e.Message);

            return rtn;
        }

        public static ResponseViewModel SyncDownloadFailResponse(Exception e, string entity)
        {
            var rtn = new ResponseViewModel
            {
                Message = string.Format("Failed To Download {0}(s) from the Election API. See Error For Details", entity),
                Success = false,
                Error = new ModelStateDictionary()
            };
            rtn.Error.AddModelError("code", e.Message);

            return rtn;
        }

        public static ResponseViewModel ContextCreationFailedResponse()
        {
            var rtn = new ResponseViewModel
            {
                Message = "Failed to create context.",
                Success = false,
                Error = new ModelStateDictionary()
            };

            rtn.Error.AddModelError("CF1000", "Unable To Connect To The Election API");

            return rtn;
        }

        public static ResponseViewModel ErrorResponse(string code, string message)
        {
            var rtn = new ResponseViewModel
            {
                Message = "Error.",
                Success = false,
                Error = new ModelStateDictionary()
            };
            rtn.Error.AddModelError(code, message);

            return rtn;
        }
    }
}
