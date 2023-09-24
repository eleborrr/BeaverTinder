namespace Contracts.Responses.Registration;

public class RegisterResponseDto: ResponseBaseDto
{
    private Dictionary<RegisterResponseStatus, string> _messages = new()
    {
        {RegisterResponseStatus.Ok, "Registration successful. Email send"},
        {RegisterResponseStatus.InvalidData, "Invalid input data"},
        {RegisterResponseStatus.SendEmailFailure, "Fail in sending email"},
        {RegisterResponseStatus.UserCreationFailure, "Failed to create user"},
        {RegisterResponseStatus.Fail, "Error"}
    };
    
    //TODO разобраться какие коды лучше вставлять. мб есть способ лучше это делать? 
    private Dictionary<RegisterResponseStatus, int> _codes = new()
    {
        {RegisterResponseStatus.Ok, 200},
        {RegisterResponseStatus.InvalidData, 400},
        {RegisterResponseStatus.SendEmailFailure, 403},
        {RegisterResponseStatus.UserCreationFailure, 403},
        {RegisterResponseStatus.Fail, 400}

    };

    public RegisterResponseDto(RegisterResponseStatus status, string? message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == RegisterResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}