namespace Contracts.Responses.Search;

public class SearchUserFailedResponse
{
    public bool Successful { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}