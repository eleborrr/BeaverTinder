namespace BeaverTinder.Application.Dto.BeaverMatchSearch;

public class SearchUserResponse
{
    public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Age { get; set; }
    public string Gender { get; set; } = default!;
    public string About { get; set; } = default!;
}