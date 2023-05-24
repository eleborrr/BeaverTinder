using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class SearchUserResultDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string About { get; set; }
}