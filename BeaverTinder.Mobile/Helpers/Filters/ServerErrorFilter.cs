using BeaverTinder.Mobile.Errors;

namespace BeaverTinder.Mobile.Helpers.Filters;

public sealed class ServerErrorFilter : IErrorFilter
{
   private readonly ILogger _logger;
   private readonly IWebHostEnvironment _environment;

   public ServerErrorFilter(ILogger logger, IWebHostEnvironment environment)
   {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _environment = environment ?? throw new ArgumentNullException(nameof(environment));
   }

   public IError OnError(IError error)
   {
      if (error.Exception is BeaverSearchError searchException)
      {
         _logger.LogError(searchException, "There is a validation error");

         var errorBuilder = ErrorBuilder
            .New()
            .SetMessage("Something went wrong")
            .SetCode(searchException.Message)
            .SetPath(error.Path);

         return errorBuilder.Build();
      }
      
      _logger.LogError(error.Exception, error.Message);

      if (_environment.IsDevelopment())
         return error;

      return ErrorBuilder
         .New()
         .SetMessage("An unexpected server fault occurred")
         .SetCode("Unexpected error. Pls contact beaver admin!")
         .SetPath(error.Path)
         .Build();
   }
}