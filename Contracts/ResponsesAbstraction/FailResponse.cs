namespace Contracts.ResponsesAbstraction;

public record FailResponse(bool Successful, string Message, int StatusCode);