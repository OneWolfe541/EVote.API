using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.Models
{
    public class ReportParametersModel
    {
        public string SQLDatabaseName { get; set; }
        public int? id { get; set; }
        public int? type { get; set; }
        public int? pollID { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? reportingDate { get; set; }
        public string reportType { get; set; }
    }
}
