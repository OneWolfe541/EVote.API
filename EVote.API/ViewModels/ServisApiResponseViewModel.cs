using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.ViewModels
{
    public class ServisApiResponseViewModel
    {
        public bool Success { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
        public ServisApiResponseErrorViewModel Error { get; set; }
        public ServisApiResponseDataViewModel Data { get; set; }
    }

    public class ServisApiResponseErrorViewModel
    {
        public string ErrorMessage { get; set; }
    }

    public class ServisApiResponseDataViewModel
    {
        public Guid AesElectionId { get; set; }
        public string CountyCode { get; set; }
        public int ElectionId { get; set; }
        public List<int> VoterIds { get; set; }
        public DateTime? MaxDate { get; set; }
        //public List<AbsenteeQueueViewModel> AbsenteeQueue { get; set; }

        //public List<VotedQueueViewModel> VotedDeleteQueue { get; set; }
        //public List<VotedQueueViewModel> VotedQueue { get; set; }
        //public List<TabulatorCreditViewModel> TabulatorCredit { get; set; }
        //public SpoiledViewModel Spoileds { get; set; }
        //public ServisElection ServisElection { get; set; }
        public int LogCode { get; set; }
    }
}
