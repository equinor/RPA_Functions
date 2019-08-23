using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rpa_functions.rpa_pc35;

namespace rpa_functions
{
    public static class PC35_Auth
    {
        [FunctionName("PC35_Auth")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PC35 Auth request handled");

            // Dummy service to enable token generation.
            return (ActionResult)new OkObjectResult("RPA Authentication successful");

        }
    }

    public static class PC35_Webservice
    {
        [FunctionName("PC35_Webservice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PC35 Webservice received a request");

            // Get HTTP data
            dynamic bodyData = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
            string method = req.Method;

            // Setup table operations object
            PackCheckTableOperations packTable = new PackCheckTableOperations();

            switch(method)
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

                    if(bodyData.Id != "")
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
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
