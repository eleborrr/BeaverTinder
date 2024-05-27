//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:async' as $async;
import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'package:protobuf/protobuf.dart' as $pb;

import 'chat.pb.dart' as $0;

export 'chat.pb.dart';

@$pb.GrpcServiceName('greet.Chat')
class ChatClient extends $grpc.Client {
  static final _$connectToRoom = $grpc.ClientMethod<$0.JoinRequest, $0.MessageGrpc>(
      '/greet.Chat/ConnectToRoom',
      ($0.JoinRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.MessageGrpc.fromBuffer(value));
  static final _$sendMessage = $grpc.ClientMethod<$0.MessageGrpc, $0.Empty>(
      '/greet.Chat/SendMessage',
      ($0.MessageGrpc value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));

  ChatClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options,
        interceptors: interceptors);

  $grpc.ResponseStream<$0.MessageGrpc> connectToRoom($0.JoinRequest request, {$grpc.CallOptions? options}) {
    return $createStreamingCall(_$connectToRoom, $async.Stream.fromIterable([request]), options: options);
  }

  $grpc.ResponseFuture<$0.Empty> sendMessage($0.MessageGrpc request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$sendMessage, request, options: options);
  }
}

@$pb.GrpcServiceName('greet.Chat')
abstract class ChatServiceBase extends $grpc.Service {
  $core.String get $name => 'greet.Chat';

  ChatServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.JoinRequest, $0.MessageGrpc>(
        'ConnectToRoom',
        connectToRoom_Pre,
        false,
        true,
        ($core.List<$core.int> value) => $0.JoinRequest.fromBuffer(value),
        ($0.MessageGrpc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.MessageGrpc, $0.Empty>(
        'SendMessage',
        sendMessage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.MessageGrpc.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
  }

  $async.Stream<$0.MessageGrpc> connectToRoom_Pre($grpc.ServiceCall call, $async.Future<$0.JoinRequest> request) async* {
    yield* connectToRoom(call, await request);
  }

  $async.Future<$0.Empty> sendMessage_Pre($grpc.ServiceCall call, $async.Future<$0.MessageGrpc> request) async {
    return sendMessage(call, await request);
  }

  $async.Stream<$0.MessageGrpc> connectToRoom($grpc.ServiceCall call, $0.JoinRequest request);
  $async.Future<$0.Empty> sendMessage($grpc.ServiceCall call, $0.MessageGrpc request);
}
