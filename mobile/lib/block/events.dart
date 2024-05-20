import 'package:flutter/material.dart';
import 'package:mobile/dto/edit_user_response_dto.dart';
import 'package:mobile/dto/geolocation_response_dto.dart';
import 'package:mobile/dto/user_subscription_dto.dart';
import 'package:mobile/instances/user.dart';

class LoadingEventBase { }

class GenericLoadingEventBase<TData> extends LoadingEventBase {
  final Widget Function(TData) builder;

  GenericLoadingEventBase({ required this.builder });
}

class EditProfileEvent extends GenericLoadingEventBase<EditUserResponseDto> {
  final User user;
  EditProfileEvent({ required super.builder, required this.user });
}

class GetGeolocationResponseEvent extends GenericLoadingEventBase<GeolocationResponseDto> {
  final String id;
  GetGeolocationResponseEvent({ required super.builder, required this.id  });
}

class GetUserSubscriptionEvent extends GenericLoadingEventBase<UserSubscriptionDto> {
  final String id;
  GetUserSubscriptionEvent({ required super.builder, required this.id  });
}

class UserProfileInfoEvent extends GenericLoadingEventBase<User> {
  UserProfileInfoEvent({ required super.builder});
}