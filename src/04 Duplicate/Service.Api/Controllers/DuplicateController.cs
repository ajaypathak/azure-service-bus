using Microsoft.AspNetCore.Mvc;

using ServiceBus.Abstraction;
using ServiceBus.Helper;
using ServiceBus.Model;

namespace Service.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/serviceBusDuplicate")]
    public class DuplicateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public DuplicateController(IConfiguration configuration, IProductService productService)
        {
            _configuration = configuration;
            _productService = productService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [Route("publishMessages")]
        [HttpPost]
        public async Task<ActionResult<List<Product>>> PublishMessageAsync([FromBody] int count)
        {
           
            var products = _productService.GetProducts();
            List<Product> productsList = new List<Product>();
            int counter = 0;
            int position = 0;
            Random random = new Random(DateTime.Now.Millisecond);

            var connectionString = _configuration["connectionString"];
            var queueName = _configuration["queueName"];

            Sender sender = new Sender(connectionString, queueName);
            while (counter < count)
            {
                //Cause random message generation
                if (random.NextDouble() > .5)
                {
                    position++;
                }

                Product product = products[position];
                await sender.SendMessageAsync(product);
                productsList.Add(product);
                counter++;

            }
            return productsList;

        }
    }
}
