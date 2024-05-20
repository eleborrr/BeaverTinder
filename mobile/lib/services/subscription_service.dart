import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/result_dto.dart';
import 'package:mobile/dto/subscription/payment_request_dto.dart';
import 'package:mobile/dto/subscription/subscription_info_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';


abstract class SubscriptionServiceBase {
  Future<Result<List<SubscriptionInfoDto>, String>> getAllSubscriptionsAsync();
  Future<Result<ResultDto, String>> paySubscriptionAsync(PaymentRequestDto model);
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

  @override
  Future<Result<ResultDto, String>> paySubscriptionAsync(PaymentRequestDto model) async {
    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation {
        pay(
          model: {
            amount: ${model.amount}
            cardNumber: "${model.cardNumber}"
            code: "${model.code}"
            month: ${model.month}
            subsId: ${model.subsId}
            userId: "${model.userId}"
            year: ${model.year}
          }
        ) {
          error
          isFailure
          isSuccess
        }
      }
      '''),
    );

    var result = await _client.mutate(options);

    var r = ResultDto.fromJson(result.data!["pay"]);
    return result.hasException
        ? const Result.fromFailure("Can not find subscriptions")
        : Result.fromSuccess(r);
  }
}