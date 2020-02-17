using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rpa_functions.rpa_pc243;
using rpa_functions.rpa_pc269;
using rpa_functions.rpa_pc35;
using rpa_functions.rpa_pc239;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Only used by pc243
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace rpa_functions
{

    public class PC239WebService
    {
        private ReturnForCreditTableOperations rfcOps = new ReturnForCreditTableOperations();

        [FunctionName("PC239_GetReturnForCredit")]
        public async Task<IActionResult> GetReturnForCredit(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PC239_GetReturnForCredit/{guid}")] HttpRequest req,
    ILogger log, string guid)
        {
            log.LogInformation("PC239 GetReturnForCredit called");

            var rfcInfo = await rfcOps.QueryReturnForCreditOnGuid(guid);
            return new OkObjectResult(JsonConvert.SerializeObject(rfcInfo)); // put something in here.
        }

        [FunctionName("PC239_PostReturnForCredit")]
        public async Task<IActionResult> PostReturnForCredit(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC239_PostReturnForCredit")] HttpRequest req,
            CancellationToken cts, ILogger log)
        {
            log.LogInformation("PC239 PostReturnForCredit called");

            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
            var retVal = await rfcOps.InsertBatch(bodyData);
            return new OkObjectResult("{ \"webID\": " + JsonConvert.SerializeObject(retVal) + "}");
        }

        [FunctionName("PC239_DeleteReturnForCredit")]
        public async Task<IActionResult> DeleteReturnForCredit(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "PC239_DeleteReturnForCredit/{guid}")] HttpRequest req,
            ILogger log, string guid)
        {
            log.LogInformation("PC239_DeleteReturnForCredit called");
            var retVal = await rfcOps.RemoveTableEntityOnGuid(guid);

            return new ObjectResult(retVal);
        }
    }

    
    public class PC269_Webservice
    {
        private readonly PC269Context _context;

        public PC269_Webservice(PC269Context context)
        {
            _context = context;
        }

        [FunctionName("PC269_GetAssetByBatch")]
        public IActionResult GetAssets(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PC269_GetAssetByBatch/{batch}")] HttpRequest req,
            ILogger log, string batch)
        {
            log.LogInformation("PC269 GetAssets called");

            var assetInfo = _context.Assets.FirstOrDefault(b => b.abbyy_batch == batch);

                                            
            return new OkObjectResult(JsonConvert.SerializeObject(assetInfo));
        }

        [FunctionName("PC269_PostAsset")]
        public async Task<IActionResult> PostAssetAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostAsset")] HttpRequest req,
            CancellationToken cts,
            ILogger log)
        {
            log.LogInformation("PC269 post assets  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            Asset newAsset = JsonConvert.DeserializeObject<Asset>(requestBody);

            var entity = await _context.Assets.AddAsync(newAsset, cts);

            await _context.SaveChangesAsync(cts);

            return new OkObjectResult(JsonConvert.SerializeObject(entity.Entity));
        }

        [FunctionName("PC269_PostDailyReport")]
        public async Task<IActionResult> PostDailyReportAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostDailyReport/{asset_id}")] HttpRequest req,
            int asset_id,
            CancellationToken cts,
            ILogger log)
        {
            log.LogInformation("PC269 post daily report  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            DailyReport newDailyReport = JsonConvert.DeserializeObject<DailyReport>(requestBody);

            newDailyReport.asset_Id = asset_id;

            var entity = await _context.DailyReports.AddAsync(newDailyReport, cts);

            await _context.SaveChangesAsync(cts);



            return new OkObjectResult(JsonConvert.SerializeObject(entity.Entity));
        }

        [FunctionName("PC269_PostWellsDetails")]
        public async Task<IActionResult> PostWellsDetailAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostWellsDetails/{dailyreport_id}")] HttpRequest req,
          int dailyreport_id,
          CancellationToken cts,
          ILogger log)
        {
            log.LogInformation("PC269 post wellstest  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic newWellstest = JsonConvert.DeserializeObject(requestBody);

            List<WellsDetails> WellDetailsList = PC269Mappings.ObjectToWellsDetailsList(newWellstest, dailyreport_id);

            foreach(WellsDetails wellDetailsElement in WellDetailsList)
            {
          
                await _context.WellsDetails.AddAsync(wellDetailsElement, cts);
               
                await _context.SaveChangesAsync(cts);
            }


            return new OkObjectResult("ok");
        }


        [FunctionName("PC269_PostWaterInjectionWell")]
        public async Task<IActionResult> PostWaterInjectionWellAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostWaterInjectionWell/{dailyreport_id}")] HttpRequest req,
          int dailyreport_id,
          CancellationToken cts,
          ILogger log)
        {
            log.LogInformation("PC269 post waterinjection  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic newWaterInjectionWell = JsonConvert.DeserializeObject(requestBody);

            List<WaterInjectionWell> WaterInjectionWellList = PC269Mappings.ObjectToWaterInjectionWellList(newWaterInjectionWell, dailyreport_id);

            foreach (WaterInjectionWell waterInjectionWell in WaterInjectionWellList)
            {

                await _context.WaterInjectionWells.AddAsync(waterInjectionWell, cts);

                await _context.SaveChangesAsync(cts);
            }


            return new OkObjectResult("ok");
        }


        [FunctionName("PC269_PostGasInjectionWell")]
        public async Task<IActionResult> PostGasInjectionWellAsync(
       [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostGasInjectionWell/{dailyreport_id}")] HttpRequest req,
        int dailyreport_id,
        CancellationToken cts,
        ILogger log)
        {
            log.LogInformation("PC269 post gasinjection  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic newGasInjectionWell = JsonConvert.DeserializeObject(requestBody);

            List<GasInjectionWell> GasInjectionWellList = PC269Mappings.ObjectToGasInjectionWellList(newGasInjectionWell, dailyreport_id);

            foreach (GasInjectionWell gasInjectionWell in GasInjectionWellList)
            {

                await _context.GasInjectionWells.AddAsync(gasInjectionWell, cts);

                await _context.SaveChangesAsync(cts);
            }


            return new OkObjectResult("ok");
        }

        [FunctionName("PC269_PostComments")]
        public async Task<IActionResult> PostCommentsAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_PostComments/{dailyreport_id}")] HttpRequest req,
        int dailyreport_id,
        CancellationToken cts,
        ILogger log)
        {
            log.LogInformation("PC269 post comments  request received");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            List<Comment> newComments = JsonConvert.DeserializeObject<List<Comment>>(requestBody);

            foreach(Comment commentElement in newComments)
            {
                commentElement.dailyreport_Id = dailyreport_id;

                await _context.Comments.AddAsync(commentElement, cts);

                await _context.SaveChangesAsync(cts);
            }
            

            return new OkObjectResult("ok");
        }


        [FunctionName("PC269_UploadFile")]
        public async Task<IActionResult> UploadFileToBlob(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC269_UploadFile")] HttpRequestMessage req,
         ILogger log)
        {
            log.LogInformation("PC269 Upload File called");
            CommonBlob blobOps = new CommonBlob();
            Stream data = await req.Content.ReadAsStreamAsync();

            DateTime _date = DateTime.Now;
            var _dateString = _date.ToString("dd-MM-yyyy");
            string fileName = $"{_dateString}-{Guid.NewGuid().ToString()}.pdf";

            Uri retUri = await blobOps.uploadFileToBlob(data, fileName);

            Console.WriteLine(retUri.AbsoluteUri);

            return new OkObjectResult(new { fileuri = retUri.AbsoluteUri });
        }
    }

    public class PC243_Webservice
    {
        private MaterialDeliveryTableOperations mdTableOps = new MaterialDeliveryTableOperations();

        // Will be called by the Customer WWW Interface (EXPOSED TO INTERNET)
        [FunctionName("PC243_GetMaterialDelivery")]
        public HttpResponseMessage GetMaterialDelivery(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PC243_MaterialDelivery/{webid}")] HttpRequest req,
           ILogger log, string webid)
        {
            log.LogInformation("PC243 Get task trigged");

            string materialDeliveryHTML = HtmlTemplate.GetPage(mdTableOps.QueryMaterialDeliveryOnWebid(webid, false).Result);

            Console.Write(materialDeliveryHTML);
            if (materialDeliveryHTML != null && materialDeliveryHTML.Length > 0)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(materialDeliveryHTML);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return null;

            }

        }

        // Will be called by the Customer WWW Interface (EXPOSED TO INTERNET)
        [FunctionName("PC243_PostMaterialDeliveryUpdate")]
        public async Task<IActionResult> PostMaterialDeliveryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "PC243_PostMaterialDeliveryUpdate/{webid}/{guid}")] HttpRequest req,
            string webid,
            string guid,
            ILogger log)
        {
            log.LogInformation("PC243 post material delivery  request received");

            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());

            Object retVal = await mdTableOps.UpdateMaterialDelivery(guid, bodyData);

            return (ActionResult)new OkObjectResult(retVal);
        }

        // Will be called by the robot (IP block and request key required)
        [FunctionName("PC243_PostMaterialDelivery")]
        public async Task<IActionResult> PostMaterialDelivery(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PC243_PostMaterialDelivery")] HttpRequest req,
          ILogger log)
        {
            log.LogInformation("PC243 post material delivery  request by robot received");
            
            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
            

            //  Check if input it OK
            
            string retVal = await mdTableOps.InsertBatch(bodyData);

            // This is messy, correct..
            return new OkObjectResult("{'webid': '"+retVal+"'}");
        }

        // Make a webservice to poll on webids on status=1 (updated), update to status 2
        [FunctionName("PC243_GetMaterialDeliveryRob")]
        public async Task<IActionResult> GetMaterialDeliveryRob(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PC243_GetMaterialDeliveryRob")] HttpRequest req, ILogger log)
        {
            //int stat = int.Parse(status);
            log.LogInformation("PC243 get material delivery  request by robot received");
            string materialDeliveryresp = JsonConvert.SerializeObject(mdTableOps.QueryMaterialDeliveryOnStatusAndComplete().Result);


            Console.Write(materialDeliveryresp);

            return new OkObjectResult(materialDeliveryresp);

        }


        // Make webservice to poll webids on status= 2 (fetched), update to status 3, and remove
        [FunctionName("PC243_RemoveMaterialDeliveryRob")]
        public async Task<IActionResult> RemoveMaterialDeliveryRob(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PC243_RemoveMaterialDeliveryRob")] HttpRequest req, ILogger log)
        {
            //int stat = int.Parse(status);
            log.LogInformation("PC243 remove material delivery  request by robot received");
            string materialDeliveryresp = JsonConvert.SerializeObject(mdTableOps.QueryMaterialDeliveryOnStatusAndRemove().Result);


            Console.Write(materialDeliveryresp);

            return new OkObjectResult(materialDeliveryresp);

        }








        // Make a webservice to expire


    }

    public class PC0035_Webservice
    {
        [FunctionName("PC35_Auth")]
        public static IActionResult Auth(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PC35 Auth request handled");

            // Dummy service to enable token generation.
            return (ActionResult)new OkObjectResult("RPA Authentication successful");
        }

        [FunctionName("PC35_Webservice")]
        public static async Task<IActionResult> Webservice(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PC35 Webservice received a request");

            // Get HTTP data
            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
            string method = req.Method;

            // Setup table operations object
            PackCheckTableOperations packTable = new PackCheckTableOperations();

            switch (method)
            {
                case "GET":
                    // Get pending packages (robot initiated)
                    log.LogInformation("GET Request");

                    string result_get = packTable.QueryPendingPackages().Result;

                    return result_get != null
                        ? (ActionResult)new OkObjectResult(result_get)
                        : new BadRequestObjectResult("No Matches");

                case "POST":
                    // Insert new packages (user initiatied)
                    log.LogInformation("POST Request");

                    string result_post = null;

                    if (bodyData != null) result_post = await packTable.InsertBatch(bodyData);

                    return result_post != null
                        ? (ActionResult)new OkObjectResult(result_post)
                        : new BadRequestObjectResult("Not valid input");

                case "PATCH":
                    log.LogInformation("PATCH Request");

                    bool result_patch = false;

                    if (bodyData.Id != "")
                    {
                        result_patch = await packTable.UpdatePackage(bodyData);
                    }

                    return result_patch != false
                        ? (ActionResult)new OkObjectResult("Success")
                        : new BadRequestObjectResult("Not valid input");

                default:
                    log.LogError("Invalid HTTP method");
                    return new BadRequestObjectResult("Not implemented, go away");
            }
        }
    }

    public static class PC185_Webservice
    {
        [FunctionName("PC185_Webservice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PC185 received a webservice call");

            string name = req.Query["name"];

            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
            string method = req.Method;

            // Setup table operations object
            PackCheckTableOperations packTable = new PackCheckTableOperations();

            switch (method)
            {
                case "GET":
                    // Get pending serials
                    log.LogInformation("GET Request");

                    string result_get = null;
                    string wakeup = req.Query["wakeup"];

                    if (wakeup == "true") result_get = "Waking up webservice...";
                    else result_get = packTable.QueryPendingPackages().Result;

                    return result_get != null
                        ? (ActionResult)new OkObjectResult(result_get)
                        : new BadRequestObjectResult("No Matches");

                case "POST":
                    // Insert new packages (user initiatied)
                    log.LogInformation("POST Request");

                    string result_post = null;

                    if (bodyData != null) result_post = await packTable.InsertBatch(bodyData);

                    return result_post != null
                        ? (ActionResult)new OkObjectResult(result_post)
                        : new BadRequestObjectResult("Not valid input");

                case "PATCH":
                    log.LogInformation("PATCH Request");

                    bool result_patch = false;

                    if (bodyData.Id != "")
                    {
                        result_patch = await packTable.UpdatePackage(bodyData);
                    }

                    return result_patch != false
                        ? (ActionResult)new OkObjectResult("Success")
                        : new BadRequestObjectResult("Not valid input");

                default:
                    log.LogError("Invalid HTTP method");
                    return new BadRequestObjectResult("Not implemented, go away");
            }
        }
    }
}