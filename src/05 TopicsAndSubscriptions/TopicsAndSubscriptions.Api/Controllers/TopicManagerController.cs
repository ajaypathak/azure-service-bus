using Azure.Messaging.ServiceBus.Administration;

using Microsoft.AspNetCore.Mvc;

using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Helper;
using TopicsAndSubscriptions.Helper.ReceiveHanlder;
using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicManagerController : ControllerBase


    {
        private readonly IConfiguration _configuration;
        private readonly IIndianStateProvider _indianStateProvider;
        private readonly IProductProvider _productProvider;
        public TopicManagerController(IConfiguration configuration, IIndianStateProvider indianStateProvider, IProductProvider productProvider)
        {
            _configuration = configuration;
            _indianStateProvider = indianStateProvider;
            _productProvider = productProvider;
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
        public async Task<ActionResult<List<SubscriptionProperties>>> CreateSubscriptionsAsyc([FromBody] string topicName = "ajaytopic")
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
        public async Task<ActionResult<bool>> PublishMessageAsyns([FromBody] Order order, string topicName = "ajaytopic")
        {
            if (order==null )
            {
                return BadRequest("Value of Order is null");
            }
            if (order.Products == null || order?.Products.Any() == false)
            {
                return BadRequest("Value of  Products is null");
            }
            if (order.Products.Any() == false)
            {
                return BadRequest("Value of  Products is empty");
            }

            var connectionString = _configuration["topicConnectionString"];
            var topicManager = new TopicSender(topicName, connectionString);
            order.TotalPrice = order.Products.Sum(x => x.Price);
            await topicManager.SendMessageAsync(order);
            return true;
        }
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("publishbulkmessage")]
        [HttpGet]
        public async Task<ActionResult<string>> PublishBulkMessageAsyns( string topicName="ajaytopic",int orderCount=100)
        {
            var connectionString = _configuration["topicConnectionString"];
            var topicManager = new TopicSender(topicName, connectionString);
            var products= _productProvider.GetProducts();
            var states=_indianStateProvider.GetAllIndianStates();
            var productCount=products.Count();
            var statesCount=states.Count();
            Random random=new Random();
            int counter = 0;
            for (int i = 0; i < orderCount; i++)
            {
                Order order = new Order();
                order.State = states[random.Next(0,statesCount)].StateCode;
                order.Products.Add(products[random.Next(0, productCount)]);
                await topicManager.SendMessageAsync(order);
                counter++;
            }
           
            return $"Published {counter} orders";
        }
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("subscription")]
        [HttpGet]
        public async Task<ActionResult<bool>> ProcessSubscriptionsAsyns(string subscriptionName,string topicName = "ajaytopic" )
        {
            var connectionString = _configuration["topicConnectionString"];
            var topicManager = new ProcessSubscriptions(connectionString, topicName, subscriptionName);
            await topicManager.ReceiveMessagesAsyns();
            return true;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("allsubscriptions")]
        [HttpGet]
        public async Task<ActionResult<bool>> ProcessAllSubscriptionsAsyns(string topicName = "ajaytopic")
        {
            var connectionString = _configuration["topicConnectionString"];
            string subscriptionName;
          
            var states = _indianStateProvider.GetAllIndianStates();

            foreach (var item in states)
            {
                subscriptionName = item.StateCode + "-Subscription";  
                var topicManager = new ProcessSubscriptions(connectionString, topicName, subscriptionName);
                await topicManager.ReceiveMessagesAsyns();

            }
          
            return true;
        }
    }
}
