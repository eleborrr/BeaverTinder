namespace BeaverTinder.Application.Dto.Authentication.Register;

public enum RegisterResponseStatus
{
    Ok,
    InvalidData,
    UserCreationFailure,
    SendEmailFailure,
    Fail
}