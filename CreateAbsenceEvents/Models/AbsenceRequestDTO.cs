using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAbsenceEvents.Models
{
    public class AbsenceRequestDTO
    {
        public string Envoirment { get; set; }
        public string OrgId { get; set; }
        public string LoginId { get; set; }
        public string Pin { get; set; }
        public string WorkerId { get; set; }
        public string NoOfAbsences { get; set; }
        public string StartDt { get; set; }
        public string AbsencesCreated { get; set; }
        public string JobId { get; set; }
        public bool Processing { get; set; }
        public List<SelectListItem> Environments { get; set; } 

        public AbsenceRequestDTO()
        {
            StartDt = "2019,1,1";
            JobId = string.Empty;
            Environments = new List<SelectListItem>
            {
                new SelectListItem { Value = "http://localhost.fldev.net:9000/", Text = "DEVELOPMENT" },
                new SelectListItem { Value = "https://aesopapiqa.flqa.net/", Text = "QA" },
                new SelectListItem { Value = "https://aesopapistage.flqa.net/", Text = "STAGE"  },
            };

        }
    }
    
}
