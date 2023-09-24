namespace Contracts.Responses.Login;

public class LoginResponseDto: ResponseBaseDto
{
    private Dictionary<LoginResponseStatus, string> _messages = new()
    {
        {LoginResponseStatus.Ok, "Login successful"},
        {LoginResponseStatus.Fail, "Invalid login attempt"}
    };
    
    //TODO разобраться какие коды лучше вставлять. мб есть способ лучше это делать? 
    private Dictionary<LoginResponseStatus, int> _codes = new()
    {
        {LoginResponseStatus.Ok, 200},
        {LoginResponseStatus.Fail, 400}

    };
    
    public LoginResponseDto(LoginResponseStatus status, string? message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == LoginResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}

