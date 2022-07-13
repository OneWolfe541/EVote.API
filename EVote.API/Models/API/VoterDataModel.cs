using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.Models
{
    public class VoterDataModel
    {
        public string VoterID { get; set; }
        public int? BarCode { get; set; }
        public Nullable<int> ElectionID { get; set; }
        public Nullable<int> ComboNo { get; set; }
        public Nullable<int> District { get; set; }
        public Nullable<int> VoterNo { get; set; }
        public string DistrictName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Generation { get; set; }
        public string MaidenName { get; set; }
        public string SirnameOrdered { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public bool InvalidRegisteredAddress { get; set; }
        public bool InvalidMailingAddress { get; set; }
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string MailingCountry { get; set; }
        public string PhysicalAddress1 { get; set; }
        public string PhysicalAddress2 { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalZip { get; set; }
        public string PhysicalCountry { get; set; }
        public string PhysicalCSZ { get; set; }
        public bool TempUsed { get; set; }
        public string TempAddress1 { get; set; }
        public string TempAddress2 { get; set; }
        public string TempCity { get; set; }
        public string TempState { get; set; }
        public string TempZip { get; set; }
        public string TempCountry { get; set; }
        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZip { get; set; }
        public string DeliveryCountry { get; set; }
        public bool OutofCountry { get; set; }
        public string TempProvince { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string DOBSearch { get; set; } 
        public string DOBYear
        {
            get
            {
                if (DOB != null)
                {
                    return DOB.Value.Year.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public Nullable<int> SpoiledReasonID { get; set; }
        public Nullable<int> LogCode { get; set; }
        public string LogDescription { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<System.DateTime> LogToday { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<System.DateTime> PrintedDate { get; set; }
        public Nullable<System.DateTime> DateVoted { get; set; }        
        public string CodeGroupState { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string LocationName { get; set; }
        public string ComputerName { get; set; }
        public Nullable<int> JurisdictionID { get; set; }
        public Nullable<int> BallotStyleID { get; set; }
        public Nullable<int> BallotNumber { get; set; }
        public string BallotStyleName { get; set; }
        public string BallotStyleFileName { get; set; }
        public Nullable<System.DateTime> ActivityDate { get; set; }
        public string UserName { get; set; }
        public bool RecAdded { get; set; }
        public bool Registered { get; set; }
        public Nullable<System.DateTime> RegisteredDate { get; set; }
        public bool ValidLocation { get; set; }
        public bool OnReservation { get; set; }
        public bool QueuedForPrint { get; set; }
        public Nullable<int> SignatureVerificationId { get; set; }
        public string SQLDatabaseName { get; set; }
    }
}
