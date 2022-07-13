using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.Models
{
    public class VoterSearchModel
    {
        public string SQLDatabaseName { get; set; }
        public string RollNumber { get; set; }
        public string VoterID { get; set; }
        public int? BarCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MarriedName { get; set; }
        public string MaidenName { get; set; }
        //public DateTime BirthDate { get; set; }
        public string BirthYear { get; set; }

        protected DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value == null)
                {
                    _birthDate = DateTime.MinValue;
                }
                else
                {
                    _birthDate = value;
                }
            }
        }

        public DateSearch SearchDate { get; set; }

        public string Month
        {
            get { return SearchDate.Month; }
            set { SearchDate.Month = value; }
        }

        public string Day
        {
            get { return SearchDate.Day; }
            set { SearchDate.Day = value; }
        }

        public string Year
        {
            get { return SearchDate.Year; }
            set { SearchDate.Year = value; }
        }

        public int? Location { get; set; }
        public int? Status { get; set; }

        public VoterSearchModel()
        {
            SearchDate = new DateSearch();
        }

        public override string ToString()
        {
            return "RollNumber=[" + RollNumber +
                "] | VoterID=[" + VoterID +
                "] | LastName=[" + LastName +
                "] | FirstName=[" + FirstName +
                "] | BirthYear=" + SearchDate.ToString() +
                "]";
        }
    }
}
