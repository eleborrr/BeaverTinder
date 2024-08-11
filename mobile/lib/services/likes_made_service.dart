import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/likes/all_likes_made_dto.dart';
import 'package:mobile/dto/likes/like_request_dto.dart';
import 'package:mobile/dto/likes/like_response_dto.dart';
import 'package:mobile/dto/likes/search_user_result_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/dio_client.dart';

abstract class LikesMadeServiceBase {
  Future<Result<AllLikesMadeDto, String>> getAllLikesMadeAsync();
}

class LikesMadeService implements LikesMadeServiceBase {

  final _client = getit<DioClient>();

  @override
  Future<Result<AllLikesMadeDto, String>> getAllLikesMadeAsync() async {
    final result = await _client.getAllLikesMade();

    return result == null
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(result);
  }
}