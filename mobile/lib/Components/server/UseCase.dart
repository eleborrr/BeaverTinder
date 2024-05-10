import 'package:mobile/Components/server/dto/register/register_response_dto.dart';

import 'auth_service.dart';
import 'dto/register/register_request_dto.dart';

class UseCase {
  final AuthService dataService;

  UseCase({required this.dataService});

  Future<RegisterResponseDto> register(RegisterRequestDto requestDto) async {
    return await dataService.register(requestDto);
  }
}