abstract class ResponseBaseDto {
  final int statusCode;
  final bool successful;
  final String message;

  ResponseBaseDto(
      this.statusCode,
      this.successful,
      this.message
      );
}