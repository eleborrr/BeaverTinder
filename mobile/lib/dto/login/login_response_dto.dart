import 'package:mobile/dto/response_base_dto.dart';

class LoginResponseDto extends ResponseBaseDto{

  LoginResponseDto(super.statusCode, super.successful, super.message);

  factory LoginResponseDto.fromJson(dynamic json) =>
      LoginResponseDto(
        json["statusCode"],
        json["successful"],
        json["message"],
      );
}