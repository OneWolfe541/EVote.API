using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public Nullable<int> RollId { get; set; }
    }
}
