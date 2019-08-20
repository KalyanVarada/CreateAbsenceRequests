using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAbsenceEvents.Models
{
    public class Login
    {
        public string loginId { get; set; }
        public string pin { get; set; }
        public bool forceRefreshSession { get; set; }
    }
}
