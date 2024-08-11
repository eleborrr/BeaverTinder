import 'package:mobile/services/account_service.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/block/events.dart';
import 'package:mobile/block/states.dart';
import 'package:mobile/main.dart';

class LoadingBloc extends Bloc<LoadingEventBase, LoadingStateBase> {

  final _accountService = getit<AccountServiceBase>();

  LoadingBloc() : super(LoadingState()) {
    on<EditProfileEvent>((event, emit) async {
      emit(LoadingState());
      final result = await _accountService.editUserInfoAsync(event.user);
      result.match(
              (s) => emit(LoadedState(data: s, builder: event.builder)),
              (f) => emit(ErrorState(error: f))
      );
    });
    on<GetGeolocationResponseEvent>((event, emit) async {
      emit(LoadingState());
      final result = await _accountService.getGeolocationByIdAsync(event.id);
      result.match(
              (s) => emit(LoadedState(data: s, builder: event.builder)),
              (f) => emit(ErrorState(error: f))
      );
    });
    on<GetUserSubscriptionEvent>((event, emit) async {
      emit(LoadingState());
      final result = await _accountService.getUserSubscriptionAsync(event.id);
      result.match(
              (s) => emit(LoadedState(data: s, builder: event.builder)),
              (f) => emit(ErrorState(error: f))
      );
    });
    on<UserProfileInfoEvent>((event, emit) async {
      emit(LoadingState());
      final result = await _accountService.getUserInfoAsync();
      result.match(
              (s) => emit(LoadedState(data: s, builder: event.builder)),
              (f) => emit(ErrorState(error: f))
      );
    });
  }
}