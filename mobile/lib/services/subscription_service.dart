import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/login/login_response_dto.dart';
import 'package:mobile/dto/subscription/subscription_info_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';


abstract class SubscriptionServiceBase {
  Future<Result<List<SubscriptionInfoDto>, String>> getAllSubscriptionsAsync();
}

class SubscriptionService implements SubscriptionServiceBase {

  final _client = getit<GraphQLClient>();


  @override
  Future<Result<List<SubscriptionInfoDto>, String>> getAllSubscriptionsAsync() async {


    final QueryOptions options = QueryOptions(
      document: gql('''
      query{
            allSubscriptions(){
                id,
                name,
                pricePerMonth,
                description
            }
          }
      '''),
    );


    final QueryResult result = await _client.query(options);

    var c = result.data!["allSubscriptions"];
    List<SubscriptionInfoDto> res = [];
    for(int i = 0; i < c.length; i++){
      res.add(SubscriptionInfoDto.fromJson(c[i]));
    }
    return result.hasException
        ? const Result.fromFailure("Can not find subscriptions")
        : Result.fromSuccess(res);
  }
}