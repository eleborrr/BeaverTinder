namespace BeaverTinder.Application.Dto.BeaverMatchSearch;

public class SearchUserResultDto
{
    public string Id { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public int Age { get; init; }
    public string Gender { get; init; } = default!;
    public string About { get; init; } = default!;
    public bool Successful { get; init; }
    public string? Message { get; init; } = default!;
    public string? DistanceInKm { get; init; } = default!;
    public int StatusCode { get; init; }
    public string Image { get; init; } = default!;
}