import 'package:dio/dio.dart';
import 'package:mobile/components/shared/beaver_auth_provider.dart';
import 'package:mobile/dto/chat_admin/chat_message_admin.dart';
import 'package:mobile/dto/chat_admin/room_data.dart';
import 'package:mobile/dto/likes/all_likes_made_dto.dart';
import 'package:mobile/main.dart';

class DioClient {
  final Dio _dio = Dio();
  final _baseClickhouseUrl = 'http://${baseIp}:8085';
  final _baseUrl = 'http://${baseIp}:4040';
  void initHeaders (){
    _dio.options.headers['Authorization'] = getit<AuthProvider>().jwtToken;
    _dio.options.headers['Accept'] = 'application/json';
  }

  Future<AllLikesMadeDto?> getAllLikesMade() async {
    AllLikesMadeDto? allLikes;
    try
    {
      Response userData = await _dio.get(_baseClickhouseUrl + '/getMadeLikes');
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
      Response userData = await _dio.get(_baseClickhouseUrl + '/connect');
      result = userData.data;
    } catch (e) {
      return null;
    }

    return result;
  }

  Future<RoomData?> getAdminRoomData() async {
    initHeaders();
    RoomData result;
    try
    {
      Response userData = await _dio.get(_baseUrl + '/im/supportChat?username=Admin');
      result = RoomData.fromJson(userData.data);
    } catch (e) {
      return null;
    }

    return result;
  }

  Future<List<ChatMessageAdmin>?> getAdminChatHistory() async {
    initHeaders();
    List<ChatMessageAdmin> result = [];
    try
    {
      Response userData = await _dio.get(_baseUrl + '/history?username=Admin');
      var c = userData.data;
      for(int i = 0; i < c.length; i++){
        result.add(ChatMessageAdmin.fromJson(c[i]));
      }
    } catch (e) {
      return null;
    }

    return result;
  }
}