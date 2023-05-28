namespace Contracts.Responses;

public class FailResponse
{
    public bool Successful { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}