import 'package:mobile/dto/response_base_dto.dart';

class LikesResponseDto extends ResponseBaseDto{

  LikesResponseDto(super.statusCode, super.successful, super.message);

  factory LikesResponseDto.fromJson(dynamic json) =>
      LikesResponseDto(
        json["statusCode"],
        json["successful"],
        json["message"],
      );
}