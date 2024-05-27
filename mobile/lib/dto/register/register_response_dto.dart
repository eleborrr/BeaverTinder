import 'package:mobile/dto/response_base_dto.dart';

class RegisterResponseDto extends ResponseBaseDto {
  RegisterResponseDto(
      super.statusCode,
      super.successful,
      super.message);


  factory RegisterResponseDto.fromJson(dynamic json) =>
      RegisterResponseDto(
        json["statusCode"],
        json["successful"],
        json["message"],
      );

}