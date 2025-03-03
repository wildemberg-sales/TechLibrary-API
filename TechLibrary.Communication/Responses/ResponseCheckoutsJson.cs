namespace TechLibrary.Communication.Responses
{
    public class ResponseCheckoutsJson
    {
        public ResponsePaginationJson Pagination { get; set; } = default!;
        public List<ResponseCheckoutJson> Checkouts { get; set; } = [];

    }
}
