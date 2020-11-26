using AutoMapper;
using Kanbersky.Couchbase.Core.DataAccess.Abstract.Couchbase;
using Kanbersky.Couchbase.Core.Models;
using Kanbersky.Couchbase.Core.Results.Exceptions.Concrete;
using Kanbersky.Couchbase.Entity.Concrete;
using Kanbersky.Couchbase.Services.Abstract;
using Kanbersky.Couchbase.Services.DTO.Request;
using Kanbersky.Couchbase.Services.DTO.Response;
using System.Threading.Tasks;

namespace Kanbersky.Couchbase.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICouchbaseRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICouchbaseRepository<Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateCustomerResponseModel> CreateCustomer(CreateCustomerRequestModel createCustomer)
        {
            var customer = _mapper.Map<Customer>(createCustomer);
            var response = await _repository.AddAsync(customer);
            return _mapper.Map<CreateCustomerResponseModel>(response);
        }

        public async Task<CustomerResponseModel> GetCustomer(string id)
        {
            var response = await _repository.FindAsync(id);
            return _mapper.Map<CustomerResponseModel>(response);
        }

        public async Task RemoveCustomer(string id)
        {
            await _repository.Remove(id);
        }

        public async Task<UpdateCustomerResponseModel> UpdateCustomer(string id, UpdateCustomerRequestModel updateCustomer)
        {
            var customer = await _repository.FindAsync(id);
            if (customer == null)
                throw BaseException.NotFoundException("Customer Not Found!");

            customer.FirstName = updateCustomer.FirstName;
            customer.LastName = updateCustomer.LastName;
            customer.Email = updateCustomer.Email;

            var response = await _repository.UpsertAsync(customer);
            return _mapper.Map<UpdateCustomerResponseModel>(response);
        }

        public async Task<PageableModel<CustomerResponseModel>> GetPageableCustomer(GetPageableCustomerRequestModel requestModel)
        {
            var response = await _repository.GetPageable(requestModel.PageSize.Value, requestModel.Page.Value);
            return _mapper.Map<PageableModel<CustomerResponseModel>>(response);
        }
    }
}
