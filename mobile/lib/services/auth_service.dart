
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/dto/login/login_request_dto.dart';
import 'package:mobile/dto/login/login_response_dto.dart';
import 'package:mobile/dto/register/register_request_dto.dart';
import 'package:mobile/dto/register/register_response_dto.dart';
import 'package:mobile/helpers/result.dart';
import 'package:mobile/main.dart';


abstract class AuthServiceBase {
  Future<Result<LoginResponseDto, String>> loginAsync(LoginRequestDto loginRequestDto);
  Future<Result<RegisterResponseDto, String>> registerAsync(RegisterRequestDto requestDto);
}

class AuthService implements AuthServiceBase {

  final _client = getit<GraphQLClient>();


  @override
  Future<Result<LoginResponseDto, String>> loginAsync(LoginRequestDto loginRequestDto) async {
    

    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation {
        login(model: {
          userName: "${loginRequestDto.userName}",
          password: "${loginRequestDto.password}",
          rememberMe: ${loginRequestDto.rememberMe},
          returnUrl: "",
        }) {
          successful
          message
          statusCode
        }
      }
    '''),
    );


    final QueryResult result = await _client.mutate(options);

    return result.hasException
        ? const Result.fromFailure("Can not login")
        : Result.fromSuccess(LoginResponseDto.fromJson(result.data!["login"]));
  }

  @override
  Future<Result<RegisterResponseDto, String>> registerAsync(RegisterRequestDto requestDto) async {
    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation {
        register(model: {
          lastName: "${requestDto.lastName}",
          firstName: "${requestDto.firstName}",
          userName: "${requestDto.userName}",
          email: "${requestDto.email}",
          dateOfBirth: "${requestDto.dateOfBirth}",
          password: "${requestDto.password}",
          confirmPassword: "${requestDto.confirmPassword}",
          gender: "${requestDto.gender}",
          about: "${requestDto.about}",
          latitude: ${requestDto.latitude},
          longitude: ${requestDto.longitude}
        }) {
          successful
          message
          statusCode
        }
      }
    '''),
    );

    final QueryResult result = await _client.mutate(options);

    return result.hasException
        ? const Result.fromFailure("Can not register")
        : Result.fromSuccess(RegisterResponseDto.fromJson(result.data!["register"]));
  }
}