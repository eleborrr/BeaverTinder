namespace Contracts.Responses;

public record FailResponse(bool Successful, string Message, int StatusCode);