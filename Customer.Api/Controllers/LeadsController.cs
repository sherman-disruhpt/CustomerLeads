using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using Customer;
using Customer.TextParser;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Customer.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static readonly CustomerCollection _collection = new CustomerCollection();

        private readonly ILogger<LeadsController> _logger;

        public LeadsController(ILogger<LeadsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Creates a Customer Lead
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /leads
        ///     {
        ///        Emmerich|Summer|trailer|HVAC|2021-07-22|+43177437725
        ///     }
        ///
        /// </remarks>
        /// <param name="data">Consists of the following 6 fields: last name, first name, property type (house, condo, trailer),  start date, project, and phone number separated by either a pipe, comma, or space delimiter</param>
        /// <returns>A newly created Customer</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or not the correct format</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerModel> CreateCustomer([FromForm] string data)
        {
            try
            {
                var badRequestMsg = "The data format is not supported";
                CustomerModel customer = null;

                if (String.IsNullOrEmpty(data))
                {
                    return BadRequest(badRequestMsg);
                }

                if (data.Contains("|"))
                {
                    customer = DataParser.Execute(data, Delimiter.Pipe);
                }
                else if (data.Contains(","))
                {
                    customer = DataParser.Execute(data, Delimiter.Comma);
                }
                else if (data.Contains(" "))
                {
                    customer = DataParser.Execute(data, Delimiter.Space);
                }

                if (customer == null)
                {
                    return BadRequest(badRequestMsg);
                }

                _collection.Add(customer);

                return Created(String.Empty, customer);

            }
            catch (Exception ex)
            {
                if (ex is DataParsingException)
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves Customer Leads sorted by property type
        /// </summary>
        /// <returns>A list of customer leads sorted by property type</returns>
        /// <response code="200">Returns a list of customer leads sorted by property type</response>
        [HttpGet("propertytype")]
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public OkObjectResult GetByPropertyType()
        {
            return new OkObjectResult(_collection.Sort(CustomerPredicate.SortByPropertyType));
        }

        /// <summary>
        /// Retrieves Customer Leads sorted by start date
        /// </summary>
        /// <returns>A list of customer leads sorted by start date</returns>
        /// <response code="200">Returns a list of customer leads sorted by start date</response>
        [HttpGet("startdate")]
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public OkObjectResult GetByStartDate()
        {
            return new OkObjectResult(_collection.Sort(CustomerPredicate.SortByStartDate));
        }

        /// <summary>
        /// Retrieves Customer Leads sorted by project
        /// </summary>
        /// <returns>A list of customer leads sorted by project</returns>
        /// <response code="200">Returns a list of customer leads sorted by project</response>
        [HttpGet("project")]
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public OkObjectResult GetByProject()
        {
            return new OkObjectResult(_collection.Sort(CustomerPredicate.SortByProject));
        }

        /// <summary>
        /// Retrieves Customer Leads duplicates from external source
        /// </summary>
        /// <returns>A list of customer leads duplicates</returns>
        /// <response code="200">Returns a list of customer leads duplicates</response>
        [HttpGet("duplicates")]
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public async Task<OkObjectResult> GetDuplicates()
        {
            var client = new RestClient("https://recruiting.leads-api.craftjack.io/leads");

            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Api-Key", _config.GetValue<string>("LeadsApiKey"));

            var response = await client.GetAsync<List<CustomerModel>>(request);

            return new OkObjectResult(_collection.Intersect(response));
        }
    }
}
