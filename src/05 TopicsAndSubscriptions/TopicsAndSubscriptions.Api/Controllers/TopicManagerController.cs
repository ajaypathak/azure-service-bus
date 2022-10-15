using Azure.Messaging.ServiceBus.Administration;

using Microsoft.AspNetCore.Mvc;

using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Helper;
using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicManagerController : ControllerBase


    {
        private readonly IConfiguration _configuration;
        private readonly IIndianStateProvider _indianStateProvider;
        public TopicManagerController(IConfiguration configuration, IIndianStateProvider indianStateProvider )
        {
            _configuration = configuration;
            _indianStateProvider = indianStateProvider;
        }
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("topic")]
        [HttpPost]
        public async Task<ActionResult<TopicProperties>> CreateTopicAsyc([FromBody] string topicName)
        {
            var connectionString = _configuration["connectionString"];
            TopicManager topicManager = new TopicManager(connectionString, _indianStateProvider);
            return await topicManager.CreateTopicAsync(topicName);
        }
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("subscriptions")]
        [HttpPost]
        public async Task<ActionResult<List<SubscriptionProperties>>> CreateSubscriptionsAsyc([FromBody] string topicName)
        {
            var connectionString = _configuration["topicConnectionString"];
            TopicManager topicManager = new TopicManager(connectionString, _indianStateProvider);
            return await topicManager.CreateSubscriptions(topicName);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("publishmessage")]
        [HttpPost]
        public async Task<ActionResult<bool>> PublishMessageAsyns([FromBody] Order order, string topicName)
        {
            var connectionString = _configuration["topicConnectionString"];
            var topicManager = new TopicSender(topicName, connectionString);
            await topicManager.SendMessageAsync(order);
            return true;
        }
    }
}
