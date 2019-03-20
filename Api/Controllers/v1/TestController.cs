using System;
using Api.Attributes;
using Data_Access.Models;
using Data_Access.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/test")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ValidateModelState]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IRepository<TestModel> _repository;
        private readonly ILogger _logger;

        public TestController(IDistributedCache distributedCache, IRepository<TestModel> testRepository, ILogger<TestController> logger)
        {
            _distributedCache = distributedCache;
            _repository = testRepository;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var request_key = "get_test_request";
            var testCache = _distributedCache.GetString(request_key);

            if (testCache == null)
            {
                var tests = _repository.FindAll();
                var testSerialized = JsonConvert.SerializeObject(tests);
                _distributedCache.SetString(request_key, testSerialized);
                return new JsonResult(tests);
            }

            _logger.LogInformation($"Test chache: {testCache}");
            var deserializedTests = JsonConvert.DeserializeObject(testCache);
            return new JsonResult(deserializedTests);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var test = _repository.FindBy<int>("id", id);
            return new JsonResult(test);
        }

        [HttpPost]
        public JsonResult Post([FromHeader] Guid idempotentKey, TestModel test)
        {
            var result = _repository.Add(test);
            return new JsonResult(result);
        }
    }
}