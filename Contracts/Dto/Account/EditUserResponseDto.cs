using Contracts.ResponsesAbstraction;

namespace Contracts.Dto.Account;

public class EditUserResponseDto: ResponseBaseDto
{
    private readonly Dictionary<EditUserResponseStatus, string> _messages = new()
    {
        {EditUserResponseStatus.Ok, "Successful editing"},
        {EditUserResponseStatus.InvalidData, "Invalid input data"},
        {EditUserResponseStatus.UserEditFailure, "Failed to edit user"},
        {EditUserResponseStatus.Fail, "Error"}
    };
    
    private readonly Dictionary<EditUserResponseStatus, int> _codes = new()
    {
        {EditUserResponseStatus.Ok, 200},
        {EditUserResponseStatus.InvalidData, 400},
        {EditUserResponseStatus.UserEditFailure, 403},
        {EditUserResponseStatus.Fail, 400}

    };

    public EditUserResponseDto(EditUserResponseStatus status, string? message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == EditUserResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}