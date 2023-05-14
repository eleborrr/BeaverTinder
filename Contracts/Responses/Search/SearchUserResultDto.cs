namespace Contracts.Responses.Search;

public class SearchUserResultDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string About { get; set; }
    public bool Successful { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}