class ResultDto {
  final bool isFailure;
  final bool isSuccess;
  final String error;

  ResultDto({
    required this.isFailure,
    required this.isSuccess,
    required this.error
  });

  factory ResultDto.fromJson(dynamic json) =>
      ResultDto(
          isFailure: json["isFailure"],
          isSuccess: json["isSuccess"],
          error: json["error"]
      );
}