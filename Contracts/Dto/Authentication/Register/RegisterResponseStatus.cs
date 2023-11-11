namespace Contracts.Dto.Authentication.Register;

public enum RegisterResponseStatus
{
    Ok,
    InvalidData,
    UserCreationFailure,
    SendEmailFailure,
    Fail
}