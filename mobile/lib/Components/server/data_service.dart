import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/Components/server/dto/login/login_request_dto.dart';
import 'package:mobile/Components/server/dto/login/login_response_dto.dart';
import 'package:mobile/Components/server/dto/register/register_response_dto.dart';
import 'package:mobile/Components/server/dto/register/register_request_dto.dart';

class DataService {
  final HttpLink httpLink = HttpLink(
    'http://192.168.56.1:5292/graphql/', // Замените на URL вашего GraphQL сервера
  );

  Future<LoginResponseDto> login(LoginRequestDto loginRequestDto) async {
    final ValueNotifier<GraphQLClient> clientNotifier = ValueNotifier(
      GraphQLClient(
        link: httpLink,
        cache: GraphQLCache(),
      ),
    );

    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation {
        register(model: {
          userName: "${loginRequestDto.userName}",
          password: "${loginRequestDto.password}",
          rememberMe: "${loginRequestDto.rememberMe}",
          returnUrl: "",
        }) {
          successful
          message
          statusCode
        }
      }
    '''),
    );


    final QueryResult result = await clientNotifier.value.mutate(options);

    // Обработка ответа от сервера
    if (result.hasException) {
      return LoginResponseDto(400,false,"not success");
      // Обработка ошибки
    } else {
      return LoginResponseDto(200,true, "some Jwt Token");
    }

  }

  Future<RegisterResponseDto> register(RegisterRequestDto requestDto) async {
    final ValueNotifier<GraphQLClient> clientNotifier = ValueNotifier(
      GraphQLClient(
        link: httpLink,
        cache: GraphQLCache(),
      ),
    );
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

    final QueryResult result = await clientNotifier.value.mutate(options);

    // Обработка ответа от сервера
    if (result.hasException) {
      return RegisterResponseDto(400,false,"not success");
      // Обработка ошибки
    } else {
      return RegisterResponseDto(200,true, "success");
    }
  }
}