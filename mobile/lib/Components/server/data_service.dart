import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/Components/server/dto/register/register_repsponse_dto.dart';
import 'package:mobile/Components/server/dto/register/register_request_dto.dart';

class DataService {
  final HttpLink httpLink = HttpLink(
    'http://192.168.0.111:5292/graphql/', // Замените на URL вашего GraphQL сервера
  );

  Future register(RegisterRequestDto requestDto) async {
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
      // Обработка ошибки
    } else {
    }
  }
}