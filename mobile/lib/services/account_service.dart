import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/edit_user_response_dto.dart';
import 'package:mobile/dto/geolocation_response_dto.dart';
import 'package:mobile/dto/user_subscription_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/instances/user.dart';
import 'package:mobile/main.dart';

abstract class AccountServiceBase {
  Future<Result<User, String>> getUserInfoAsync(String id);
  Future<Result<UserSubscriptionDto, String>> getUserSubscriptionAsync(String id);
  Future<Result<EditUserResponseDto, String>> editUserInfoAsync(User user);
  Future<Result<GeolocationResponseDto, String>> getGeolocationByIdAsync(String id);
}

class AccountService implements AccountServiceBase {

  final _client = getit<GraphQLClient>();

  @override
  Future<Result<User, String>> getUserInfoAsync(String id) async {
    final result = await _client.query(
        QueryOptions(document: gql('''
          query{
            accountInformation(
              id: "$id"
              ) { 
                lastName,
                firstName,
                userName,
                latitude,
                longitude,
                image,
                password,
                confirmPassword,
                gender,
                about,
                subName,
                subExpiresDateTime
              }
          }
        '''))
    );

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(User.fromJson(result.data));
  }

  @override
  Future<Result<GeolocationResponseDto, String>> getGeolocationByIdAsync(String id) async {
    final result = await _client.query(
        QueryOptions(
            document: gql('''
      query{
        userGeolocation(model: {
          userId: "$id"
        }){
          id,
          userId,
          latitude,
          longitude
        }
      }
    ''')
    ));

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(GeolocationResponseDto.fromJson(result.data));
  }

  @override
  Future<Result<UserSubscriptionDto, String>> getUserSubscriptionAsync(String id) async {
    final result = await _client.query(
        QueryOptions(
            document: gql('''
      query{
        userSubInformation(
          userId: "$id"
          ){
            id,
            name,
            expires
        }
      }
    ''')
        ));

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(UserSubscriptionDto.fromJson(result.data));
  }

  @override
  Future<Result<EditUserResponseDto, String>> editUserInfoAsync(User user) async {
    final result = await _client.query(
        QueryOptions(
            document: gql('''
      mutation{
        editAccount(model: {
        lastName: "${user.lastName}",
        firstName: "${user.firstName}",
        userName: "${user.userName}",
        latitude: ${user.latitude}
        longitude: ${user.longitude}
        image: "${user.image}",
        password: "${user.password}",
        confirmPassword: "${user.confirmPassword}",
        gender: "${user.gender}",
        about: "${user.about}",
        subName: "${user.subName}",
        subExpiresDateTime: "${user.subExpiresDateTime}"
        }){
          successful,
          message,
          statusCode
        }
      }
      ''')));

    return result.hasException
        ? const Result.fromFailure("Не удалось загрузить контент")
        : Result.fromSuccess(EditUserResponseDto.fromJson(result.data));
  }
}