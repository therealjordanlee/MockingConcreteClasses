using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MockingConcreteClasses.Models;
using MockingConcreteClasses.Services;

namespace MockingConcreteClasses
{
    public class DemoFunction
    {
        private ILogger _logger;
        private IRandomService _randomService;
        public DemoFunction(ILogger<DemoFunction> logger, IRandomService randomService)
        {
            _logger = logger;
            _randomService = randomService;
        }

        [FunctionName("DemoFunction")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TestModel>(requestBody);

            try
            {
                await _randomService.AddTestEntity(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed with exception: {ex.Message}");
                throw;
            }
            

            return new OkResult();
        }
    }
}
