using AutoMapper;
using Kanbersky.Couchbase.Core.Models;
using Kanbersky.Couchbase.Entity.Concrete;
using Kanbersky.Couchbase.Services.DTO.Request;
using Kanbersky.Couchbase.Services.DTO.Response;

namespace Kanbersky.Couchbase.Services.Mappings.AutoMapper
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Customer, CreateCustomerRequestModel>().ReverseMap();
            CreateMap<Customer, CreateCustomerResponseModel>().ReverseMap();

            CreateMap<Customer, UpdateCustomerResponseModel>().ReverseMap();

            CreateMap<Customer, CustomerResponseModel>().ReverseMap();

            CreateMap<PageableModel<Customer>, PageableModel<CustomerResponseModel>>().ReverseMap(); 
        }
    }
}
