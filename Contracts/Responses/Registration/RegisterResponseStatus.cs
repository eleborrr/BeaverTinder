namespace Contracts.Responses.Registration;

public enum RegisterResponseStatus
{
    Ok,
    InvalidData,
    UserCreationFailure,
    SendEmailFailure,
    Fail
}