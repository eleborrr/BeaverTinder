namespace Contracts.Responses.Login;

public class LoginResponseDto: ResponseBaseDto
{
    private Dictionary<LoginResponseStatus, string> Messages = new()
    {
        {LoginResponseStatus.Ok, "Login successful"},
        {LoginResponseStatus.Fail, "Invalid login attempt"}
    };
    
    //TODO разобраться какие коды лучше вставлять. мб есть способ лучше это делать? 
    private Dictionary<LoginResponseStatus, int> Codes = new()
    {
        {LoginResponseStatus.Ok, 200},
        {LoginResponseStatus.Fail, 400}

    };
    
    public LoginResponseDto(LoginResponseStatus status, string message="")
    {
        Message = message != "" ? message : Messages[status];
        Successful = status == LoginResponseStatus.Ok;
        StatusCode = Codes[status];
    }
}

