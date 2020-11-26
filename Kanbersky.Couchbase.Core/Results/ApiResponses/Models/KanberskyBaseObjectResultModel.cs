namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Models
{
    public class KanberskyBaseObjectResultModel<T>
    {
        public T Result { get; set; }

        public KanberskyBaseObjectResultModel(T result)
        {
            Result = result;
        }
    }
}
