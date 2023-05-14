using Contracts.Responses.Login;

namespace Contracts.Responses.Search;

public class LikeResponseDto: ResponseBaseDto
{
    private Dictionary<LikeResponseStatus, string> Messages = new()
    {
        {LikeResponseStatus.Ok, "successful"},
        {LikeResponseStatus.Fail, "Invalid like attempt"}
    };
    
    private Dictionary<LikeResponseStatus, int> Codes = new()
    {
        {LikeResponseStatus.Ok, 200},
        {LikeResponseStatus.Fail, 400}
    };
    
    public LikeResponseDto(LikeResponseStatus status, string message="")
    {
        Message = message != "" ? message : Messages[status];
        Successful = status == LikeResponseStatus.Ok;
        StatusCode = Codes[status];
    }
}