namespace Kanbersky.Couchbase.Services.DTO.Request
{
    public class GetPageableCustomerRequestModel
    {
        public int? PageSize { get; set; } = 10;

        public int? Page { get; set; } = 1;
    }
}
