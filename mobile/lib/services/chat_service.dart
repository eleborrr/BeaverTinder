import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/chats/allchat_response_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';

abstract class ChatServiceBase {
  Future<Result<List<AllChatResponseDto>, String>> getAllChatAsync();
}

class ChatService implements ChatServiceBase {
  final _client = getit<GraphQLClient>();

  @override
  Future<Result<List<AllChatResponseDto>, String>> getAllChatAsync() async {
    final QueryOptions options = QueryOptions(
      fetchPolicy: FetchPolicy.networkOnly,
      document: gql('''
        query {
          chats {
            firstName
            image
            lastName
            userName
          }
        }
      '''),
    );

    final QueryResult result = await _client.query(options);

    if (result.hasException) {
      return const Result.fromFailure("Cannot get chats");
    }

    final List<dynamic> data = result.data?["chats"] ?? [];
    final List<AllChatResponseDto> chats = data
        .map((item) => AllChatResponseDto.fromJson(item as Map<String, dynamic>))
        .toList();

    return Result.fromSuccess(chats);
  }
}
