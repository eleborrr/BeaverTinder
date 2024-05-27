import 'package:dio/dio.dart';
import 'package:mobile/dto/likes/all_likes_made_dto.dart';
import 'package:mobile/main.dart';

class DioClient {
  final Dio _dio = Dio();

  final _baseUrl = 'http://${baseIp}:8085';

  Future<AllLikesMadeDto?> getAllLikesMade() async {
    AllLikesMadeDto? allLikes;
    try
    {
      Response userData = await _dio.get(_baseUrl + '/getMadeLikes');
      allLikes = AllLikesMadeDto.fromJson(userData.data);
    } catch (e) {
      return null;
    }

    return allLikes;
  }

  Future<String?> connectUserToQueue() async {
    String result;
    try
    {
      Response userData = await _dio.get(_baseUrl + '/connect');
      result = userData.data;
    } catch (e) {
      return null;
    }

    return result;
  }
}