import 'package:mobile/dto/chat_for_two/single_chat_get_response_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';
import 'package:graphql_flutter/graphql_flutter.dart';

abstract class ChatForTwoServiceBase {
  Future<Result<SingleChatGetResponse, String>> getChatAsync(String userName);
}

class ChatForTwoService implements ChatForTwoServiceBase {
  final _client = getit<GraphQLClient>();

  @override
  Future<Result<SingleChatGetResponse, String>> getChatAsync(String userName)
    async {
      final QueryOptions options = QueryOptions(
        fetchPolicy: FetchPolicy.networkOnly,
        document: gql('''
        query {
          chat(username: "$userName") {
            receiverName
            roomName
            senderName
          }
        }
      '''),
      );

      final QueryResult result = await _client.query(options);

      return result.hasException
          ? const Result.fromFailure("Can not get chat")
          : Result.fromSuccess(SingleChatGetResponse.fromJson(result.data!["chat"]));
    }

}