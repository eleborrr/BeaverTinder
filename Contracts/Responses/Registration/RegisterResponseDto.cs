namespace Contracts.Responses.Registration;

public class RegisterResponseDto: ResponseBaseDto
{
    private Dictionary<RegisterResponseStatus, string> Messages = new()
    {
        {RegisterResponseStatus.Ok, "Registration successful. Email send"},
        {RegisterResponseStatus.InvalidData, "Invalid input data"},
        {RegisterResponseStatus.SendEmailFailure, "Fail in sending email"},
        {RegisterResponseStatus.UserCreationFailure, "Failed to create user"},
        {RegisterResponseStatus.Fail, "Error"}
    };
    
    private Dictionary<RegisterResponseStatus, int> Codes = new()
    {
        {RegisterResponseStatus.Ok, 200},
        {RegisterResponseStatus.InvalidData, 400},
        {RegisterResponseStatus.SendEmailFailure, 403},
        {RegisterResponseStatus.UserCreationFailure, 403},
        {RegisterResponseStatus.Fail, 400}

    };

    public RegisterResponseDto(RegisterResponseStatus status, string message="")
    {
        Message = message != "" ? message : Messages[status];
        Successful = status == RegisterResponseStatus.Ok;
        StatusCode = Codes[status];
    }
    
    // public RegisterResponseDto(RegisterResponseStatus status, string message)
    // {
    //     Message = message;
    //     Successful = status == RegisterResponseStatus.Ok;
    //     StatusCode = (int)status;
    // }
}