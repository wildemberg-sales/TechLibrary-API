namespace TechLibrary.Communication.Requests
{
    public class RequestFilterBooksJson
    {
        public int PageNumber { get; set; } = 0;
        public string? Title { get; set; }
    }
}
