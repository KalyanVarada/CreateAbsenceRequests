using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAbsenceEvents.Models
{
    public class LoginResponse
    {
        public class Data
        {
            public string token { get; set; }
        }

        public class ModelState
        {
        }
        public Data data { get; set; }
        public List<object> messages { get; set; }
        public bool success { get; set; }
        public ModelState modelState { get; set; }
       
    }
}
