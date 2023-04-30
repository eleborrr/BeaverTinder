namespace Contracts.Responses;

public abstract class ResponseBaseDto
{
    public int StatusCode { get; set; }
    public bool Successful { get; set; }
    
    public string Message { get; set; }
}