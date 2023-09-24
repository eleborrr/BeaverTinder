namespace Contracts.Responses.Account;

public class EditUserResponseDto: ResponseBaseDto
{
    private Dictionary<EditResponseStatus, string> _messages = new()
    {
        {EditResponseStatus.Ok, "Successful editing"},
        {EditResponseStatus.InvalidData, "Invalid input data"},
        {EditResponseStatus.UserEditFailure, "Failed to edit user"},
        {EditResponseStatus.Fail, "Error"}
    };
    
    //TODO разобраться какие коды лучше вставлять. мб есть способ лучше это делать? 
    private Dictionary<EditResponseStatus, int> _codes = new()
    {
        {EditResponseStatus.Ok, 200},
        {EditResponseStatus.InvalidData, 400},
        {EditResponseStatus.UserEditFailure, 403},
        {EditResponseStatus.Fail, 400}

    };

    public EditUserResponseDto(EditResponseStatus status, string? message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == EditResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}