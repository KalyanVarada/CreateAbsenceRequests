using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAbsenceEvents.Models
{
    public partial class AbsenceRequestModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("needSub")]
        public bool NeedSub { get; set; }

        [JsonProperty("worker")]
        public Worker Worker { get; set; }

        [JsonProperty("substitute")]
        public object Substitute { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("notesToAdmin")]
        public string NotesToAdmin { get; set; }

        [JsonProperty("notesToAdminOnly")]
        public string NotesToAdminOnly { get; set; }

        [JsonProperty("holdOption")]
        public string HoldOption { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("dbLastUpdatedTimeStamp")]
        public DateTimeOffset DbLastUpdatedTimeStamp { get; set; }

        [JsonProperty("absences")]
        public List<Absence> Absences { get; set; }
    }

    public partial class Absence
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("institution")]
        public Institution Institution { get; set; }

        [JsonProperty("entitlement")]
        public Worker Entitlement { get; set; }

        [JsonProperty("shiftType")]
        public long ShiftType { get; set; }

        [JsonProperty("accountingCode")]
        public object AccountingCode { get; set; }

        [JsonProperty("payCode")]
        public object PayCode { get; set; }

        [JsonProperty("budgetCode")]
        public object BudgetCode { get; set; }

        [JsonProperty("absenceStartTime")]
        public string AbsenceStartTime { get; set; }

        [JsonProperty("absenceEndTime")]
        public string AbsenceEndTime { get; set; }

        [JsonProperty("substituteStartTime")]
        public string SubstituteStartTime { get; set; }

        [JsonProperty("substituteEndTime")]
        public string SubstituteEndTime { get; set; }

        [JsonProperty("substituteShiftType")]
        public long SubstituteShiftType { get; set; }

        [JsonProperty("nonPayTime")]
        public string NonPayTime { get; set; }

        [JsonProperty("userSpecifiedAbsenceDuration")]
        public string UserSpecifiedAbsenceDuration { get; set; }

        [JsonProperty("hoursSubWorked")]
        public object HoursSubWorked { get; set; }

        [JsonProperty("timesAreLinked")]
        public bool TimesAreLinked { get; set; }

        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonProperty("UpdateReconcileState")]
        public object UpdateReconcileState { get; set; }
    }

    public partial class Worker
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Institution
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("startTime")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTimeOffset EndTime { get; set; }

        [JsonProperty("HoursPerDay")]
        public long HoursPerDay { get; set; }

        [JsonProperty("MaxAbsenceDuration")]
        public long MaxAbsenceDuration { get; set; }

        [JsonProperty("MinAbsenceDuration")]
        public long MinAbsenceDuration { get; set; }
    }
}
