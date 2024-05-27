import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/likes/like_request_dto.dart';
import 'package:mobile/dto/likes/like_response_dto.dart';
import 'package:mobile/dto/likes/search_user_result_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';

abstract class LikesServiceBase {
  Future<Result<SearchUserResultDto, String>> getNewUserAsync();
  Future<Result<LikesResponseDto, String>> likeUserAsync(LikeRequestDto model);
  Future<Result<LikesResponseDto, String>> dislikeAsync(LikeRequestDto model);
}

class LikesService implements LikesServiceBase {

  final _client = getit<GraphQLClient>();

  @override
  Future<Result<LikesResponseDto, String>> dislikeAsync(LikeRequestDto model) async {
    final result = await _client.mutate(
        MutationOptions(
            document: gql('''
      mutation {
        disLike(likeRequestDto: { likedUserId: "${model.likedUserId}" }) {
          message
          statusCode
          successful
        }
      }
    ''')
        ));

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(LikesResponseDto.fromJson(result.data!["disLike"]));
  }

  @override
  Future<Result<SearchUserResultDto, String>> getNewUserAsync() async {
    final result = await _client.query(
        QueryOptions(
          fetchPolicy: FetchPolicy.networkOnly,
          document: gql('''
          query {
            search {
              about
              age
              distanceInKm
              firstName
              gender
              id
              image
              lastName
              message
              statusCode
              successful
            }
          }
        '''),
        )
    );
    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(SearchUserResultDto.fromJson(result.data!["search"]));
  }

  @override
  Future<Result<LikesResponseDto, String>> likeUserAsync(LikeRequestDto model) async {
    final result = await _client.mutate(
        MutationOptions(
            document: gql('''
      mutation {
        like(likeRequestDto: { likedUserId: "${model.likedUserId}" }) {
          message
          statusCode
          successful
        }
      }
    ''')
        ));

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(LikesResponseDto.fromJson(result.data!["like"]));
  }
}