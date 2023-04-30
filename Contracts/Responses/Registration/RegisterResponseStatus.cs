namespace Contracts.Responses.Registration;

public enum RegisterResponseStatus
{
    Ok = 200,
    InvalidData = 400,
    UserCreationFailure = 403,
    SendEmailFailure = 403,
    Fail = 400
}