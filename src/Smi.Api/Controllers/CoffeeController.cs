using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smi.Api.Configuration;

namespace Smi.Api.Controllers
{
    [Route("api/coffee")]
    public class CoffeeController : ControllerBase
    {
        /// <summary> <see cref="IOptionsMonitor{TOptions}"/> reads options from config and monitors config source for changes </summary>
        private IOptionsMonitor<CoffeeOptions> _coffeeOptions;
        private readonly ILogger<CoffeeController> _logger;
        private CoffeeOptions _options;

        public CoffeeController(
            IOptionsMonitor<CoffeeOptions> coffeeOptions,
            ILogger<CoffeeController> logger)
        {
            _coffeeOptions = coffeeOptions;
            _options = _coffeeOptions.CurrentValue;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCoffee()
        {
            _logger.LogInformation("Getting coffe with options");
            return Ok(_options);
        }
    }
}
