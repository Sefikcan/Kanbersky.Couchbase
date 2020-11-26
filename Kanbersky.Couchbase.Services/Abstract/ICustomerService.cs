using Kanbersky.Couchbase.Core.Models;
using Kanbersky.Couchbase.Services.DTO.Request;
using Kanbersky.Couchbase.Services.DTO.Response;
using System.Threading.Tasks;

namespace Kanbersky.Couchbase.Services.Abstract
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponseModel> CreateCustomer(CreateCustomerRequestModel createCustomer);

        Task<UpdateCustomerResponseModel> UpdateCustomer(string id, UpdateCustomerRequestModel updateCustomer);

        Task RemoveCustomer(string id);

        Task<CustomerResponseModel> GetCustomer(string id);

        Task<PageableModel<CustomerResponseModel>> GetPageableCustomer(GetPageableCustomerRequestModel requestModel);
    }
}
