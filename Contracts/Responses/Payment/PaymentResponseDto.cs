namespace Contracts.Responses.Payment;

public class PaymentResponseDto
{
    public string Message { get; set; }
    public bool Successful { get; set; }
    public int StatusCode { get; set; }
    private Dictionary<PaymentResponseStatus, string> Messages = new()
    {
        {PaymentResponseStatus.Ok, "Payment successful"},
        {PaymentResponseStatus.InvalidData, "billing information is incorrect"},
        {PaymentResponseStatus.Fail, "Payment process fail"}
    };
    
    private Dictionary<PaymentResponseStatus, int> Codes = new()
    {
        {PaymentResponseStatus.Ok, 200},
        {PaymentResponseStatus.InvalidData, 400},
        {PaymentResponseStatus.Fail, 400}
    };
    
    public PaymentResponseDto(PaymentResponseStatus status, string message="")
    {
        Message = message != "" ? message : Messages[status];
        Successful = status == PaymentResponseStatus.Ok;
        StatusCode = Codes[status];
    }
}