namespace Contracts.Responses.Registration;

public class RegisterResponseDto: ResponseBaseDto
{
    private Dictionary<RegisterResponseStatus, string> Messages = new()
    {
        {RegisterResponseStatus.Ok, "Registration successful. Email send"},
        {RegisterResponseStatus.InvalidData, "Invalid input data"},
        {RegisterResponseStatus.SendEmailFailure, "Fail in sending email"},
        {RegisterResponseStatus.UserCreationFailure, "Failed to create user"}
    };

    public RegisterResponseDto(RegisterResponseStatus status)
    {
        Message = Messages[status];
        Successful = status == RegisterResponseStatus.Ok;
        StatusCode = (int)status;
    }
    
    public RegisterResponseDto(RegisterResponseStatus status, string message)
    {
        Message = message;
        Successful = status == RegisterResponseStatus.Ok;
        StatusCode = (int)status;
    }
}