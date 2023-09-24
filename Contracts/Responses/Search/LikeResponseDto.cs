﻿using Contracts.Responses.Login;

namespace Contracts.Responses.Search;

public class LikeResponseDto: ResponseBaseDto
{
    private Dictionary<LikeResponseStatus, string> _messages = new()
    {
        {LikeResponseStatus.Ok, "successful"},
        {LikeResponseStatus.Fail, "Invalid like attempt"}
    };
    
    private Dictionary<LikeResponseStatus, int> _codes = new()
    {
        {LikeResponseStatus.Ok, 200},
        {LikeResponseStatus.Fail, 400}
    };
    
    public LikeResponseDto(LikeResponseStatus status, string? message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == LikeResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}