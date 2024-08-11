using System.Runtime.Serialization;

namespace BeaverTinder.Mobile.Errors;

public class BeaverSearchError: Exception
{
    public BeaverSearchError() { }
    public BeaverSearchError(string message) : base(message) { }
    public BeaverSearchError(string message, Exception inner) : base(message, inner) { }
    protected BeaverSearchError(
        SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    public string Message { get; set; }

    public static BeaverSearchError WithMessage(string message)
        => new BeaverSearchError { Message = message };
}