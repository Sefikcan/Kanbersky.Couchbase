using FluentValidation;
using Kanbersky.Couchbase.Services.DTO.Request;

namespace Kanbersky.Couchbase.Services.ValidationRules.FluentValidations
{
    public class GetPageableCustomerValidator : AbstractValidator<GetPageableCustomerRequestModel>
    {
        public GetPageableCustomerValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than zero!");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than zero!");
        }
    }
}
