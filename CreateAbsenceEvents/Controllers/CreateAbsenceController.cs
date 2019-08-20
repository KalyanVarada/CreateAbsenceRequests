using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreateAbsenceEvents.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR;

namespace CreateAbsenceEvents.Controllers
{
    public class CreateAbsenceController : Controller
    {
        private IOptions<AbsenceRequestModel> absenceRequestModelSettings;
        private static HttpClient Client = new HttpClient();
        private readonly IHubContext<JobProgressHub> hubContext;

        public CreateAbsenceController(IOptions<AbsenceRequestModel> absenceRequestModelSettings, IHubContext<JobProgressHub> hubContext)
        {
            this.absenceRequestModelSettings = absenceRequestModelSettings;
            this.hubContext = hubContext;

        }
        [HttpGet]
        public IActionResult AbsenceRequest(AbsenceRequestDTO absenceRequest)
        {
            return View("AbsenceRequest", absenceRequest);
        }

        [HttpPost]
        public IActionResult Create(AbsenceRequestDTO absenceRequest)
        {
            try
            {
                string jobId = Guid.NewGuid().ToString("N");
                Task.Run(() => CreateAbsences(absenceRequest, jobId));
                absenceRequest.Processing = true;
                absenceRequest.AbsencesCreated = jobId;
                
                return View("AbsenceRequest", absenceRequest);
                //return RedirectToAction("AbsenceRequest", absenceRequest);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        private async Task CreateAbsences(AbsenceRequestDTO absenceRequest, string jobId)
        {
            int createdAbsences = 0;
            try
            {
                var absData = absenceRequestModelSettings.Value;
                var aesopAPI = absenceRequest.Envoirment + "api/v1.0/";
                var StartDate = string.IsNullOrEmpty(absenceRequest.StartDt) ? DateTime.Now : DateTime.Parse(absenceRequest.StartDt).AddDays(-1);
                string aesoptoken = "";
                
                var login = new Login();
                login.loginId = absenceRequest.LoginId;
                login.pin = absenceRequest.Pin;
                var json = JsonConvert.SerializeObject(login);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await Client.PostAsync(aesopAPI + "Login", stringContent);

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    var loginData = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
                    aesoptoken = loginData.data.token;
                    Client.DefaultRequestHeaders.Clear();
                    Client.DefaultRequestHeaders.Add("Accept", @"application/json, text/plain, */*");
                    Client.DefaultRequestHeaders.Add("AesopToken", aesoptoken);
                    var workerId = long.Parse(absenceRequest.WorkerId);
                    var InstitutionId = long.Parse(absenceRequest.OrgId);
                    var url = aesopAPI + "AbsenceReasons?workid=" + workerId + "&instid=" + InstitutionId;
                    result = await Client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        jsonString = await result.Content.ReadAsStringAsync();
                        var absenseReason = JsonConvert.DeserializeObject<AbsenceReasonResponse>(jsonString);
                        var entitlementID = long.Parse(absenseReason.data.FirstOrDefault().id);

                        url = aesopAPI + "AbsenceRequests";
                        for (int counter = 1; counter <= int.Parse(absenceRequest.NoOfAbsences); counter++)
                        {
                            var absence = absData.Absences.FirstOrDefault();
                            absence.Institution.Id = InstitutionId;
                            absData.Worker.Id = workerId;
                            absence.Date = StartDate.AddDays(counter).ToShortDateString();
                            absence.Entitlement.Id = entitlementID;
                            //to do set entitlementid
                            json = JsonConvert.SerializeObject(absData);
                            stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                            result = await Client.PostAsync(url, stringContent);
                            if (result.IsSuccessStatusCode)
                            {
                                createdAbsences++;
                                await hubContext.Clients.Groups(jobId).SendAsync("progress", createdAbsences);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString()); 
            }
            finally
            {
                await hubContext.Clients.Groups(jobId).SendAsync("progress", createdAbsences, true);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
