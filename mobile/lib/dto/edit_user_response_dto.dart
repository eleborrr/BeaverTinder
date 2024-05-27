class EditUserResponseDto {
  final bool successful;
  final String message;
  final int status;

  EditUserResponseDto({
    required this.successful,
    required this.message,
    required this.status
  });

  factory EditUserResponseDto.fromJson(dynamic json) =>
      EditUserResponseDto(
        successful: json["successful"],
        message: json["message"],
        status: json["status"]
      );
}