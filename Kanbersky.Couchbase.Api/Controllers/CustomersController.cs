using System.Threading.Tasks;
using Kanbersky.Couchbase.Core.Models;
using Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete;
using Kanbersky.Couchbase.Services.Abstract;
using Kanbersky.Couchbase.Services.DTO.Request;
using Kanbersky.Couchbase.Services.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Api.Controllers
{
    /// <summary>
    /// Customer Controller Operations
    /// </summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : KanberskyControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerService"></param>
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Create Customer Operations
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCustomerResponseModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestModel createCustomerRequest)
        {
            var response = await _customerService.CreateCustomer(createCustomerRequest);
            return ApiCreated(response);
        }

        /// <summary>
        /// Update Customer Operations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCustomerRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(UpdateCustomerResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] string id, [FromBody] UpdateCustomerRequestModel updateCustomerRequest)
        {
            var response = await _customerService.UpdateCustomer(id, updateCustomerRequest);
            return ApiUpdated(response);
        }

        /// <summary>
        /// Get Customer By Id Operations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerById([FromRoute] string id)
        {
            var response = await _customerService.GetCustomer(id);
            return ApiOk(response);
        }

        /// <summary>
        /// Delete Customer By Id Operations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
        {
            await _customerService.RemoveCustomer(id);
            return ApiNoContent();
        }

        /// <summary>
        /// Get Pageable Customer Operations
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageableModel<CustomerResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPageableCustomer([FromQuery] GetPageableCustomerRequestModel requestModel)
        {
            var response = await _customerService.GetPageableCustomer(requestModel);
            return ApiOk(response);
        }
    }
}
