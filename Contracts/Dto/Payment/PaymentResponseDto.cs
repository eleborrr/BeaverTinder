namespace Contracts.Dto.Payment;

public class PaymentResponseDto
{
    public string Message { get; set; }
    public bool Successful { get;  set; }
    public int StatusCode { get; set; }
    private readonly Dictionary<PaymentResponseStatus, string> _messages = new()
    {
        {PaymentResponseStatus.Ok, "Payment successful"},
        {PaymentResponseStatus.InvalidData, "billing information is incorrect"},
        {PaymentResponseStatus.Fail, "Payment process fail"}
    };
    
    private readonly Dictionary<PaymentResponseStatus, int> _codes = new()
    {
        {PaymentResponseStatus.Ok, 200},
        {PaymentResponseStatus.InvalidData, 400},
        {PaymentResponseStatus.Fail, 400}
    };
    
    public PaymentResponseDto(PaymentResponseStatus status, string message="")
    {
        Message = message != "" ? message : _messages[status];
        Successful = status == PaymentResponseStatus.Ok;
        StatusCode = _codes[status];
    }
}