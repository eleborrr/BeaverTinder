//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

class JoinRequest extends $pb.GeneratedMessage {
  factory JoinRequest({
    $core.String? roomName,
    $core.String? userName,
  }) {
    final $result = create();
    if (roomName != null) {
      $result.roomName = roomName;
    }
    if (userName != null) {
      $result.userName = userName;
    }
    return $result;
  }
  JoinRequest._() : super();
  factory JoinRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory JoinRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'JoinRequest', package: const $pb.PackageName(_omitMessageNames ? '' : 'greet'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'roomName')
    ..aOS(2, _omitFieldNames ? '' : 'userName')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  JoinRequest clone() => JoinRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  JoinRequest copyWith(void Function(JoinRequest) updates) => super.copyWith((message) => updates(message as JoinRequest)) as JoinRequest;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static JoinRequest create() => JoinRequest._();
  JoinRequest createEmptyInstance() => create();
  static $pb.PbList<JoinRequest> createRepeated() => $pb.PbList<JoinRequest>();
  @$core.pragma('dart2js:noInline')
  static JoinRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<JoinRequest>(create);
  static JoinRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get roomName => $_getSZ(0);
  @$pb.TagNumber(1)
  set roomName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRoomName() => $_has(0);
  @$pb.TagNumber(1)
  void clearRoomName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get userName => $_getSZ(1);
  @$pb.TagNumber(2)
  set userName($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasUserName() => $_has(1);
  @$pb.TagNumber(2)
  void clearUserName() => clearField(2);
}

class ClientMessageChat extends $pb.GeneratedMessage {
  factory ClientMessageChat({
    $core.String? text,
    $core.String? userName,
  }) {
    final $result = create();
    if (text != null) {
      $result.text = text;
    }
    if (userName != null) {
      $result.userName = userName;
    }
    return $result;
  }
  ClientMessageChat._() : super();
  factory ClientMessageChat.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ClientMessageChat.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ClientMessageChat', package: const $pb.PackageName(_omitMessageNames ? '' : 'greet'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'text')
    ..aOS(2, _omitFieldNames ? '' : 'userName')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ClientMessageChat clone() => ClientMessageChat()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ClientMessageChat copyWith(void Function(ClientMessageChat) updates) => super.copyWith((message) => updates(message as ClientMessageChat)) as ClientMessageChat;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ClientMessageChat create() => ClientMessageChat._();
  ClientMessageChat createEmptyInstance() => create();
  static $pb.PbList<ClientMessageChat> createRepeated() => $pb.PbList<ClientMessageChat>();
  @$core.pragma('dart2js:noInline')
  static ClientMessageChat getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ClientMessageChat>(create);
  static ClientMessageChat? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get text => $_getSZ(0);
  @$pb.TagNumber(1)
  set text($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasText() => $_has(0);
  @$pb.TagNumber(1)
  void clearText() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get userName => $_getSZ(1);
  @$pb.TagNumber(2)
  set userName($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasUserName() => $_has(1);
  @$pb.TagNumber(2)
  void clearUserName() => clearField(2);
}

class Empty extends $pb.GeneratedMessage {
  factory Empty() => create();
  Empty._() : super();
  factory Empty.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Empty.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Empty', package: const $pb.PackageName(_omitMessageNames ? '' : 'greet'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Empty clone() => Empty()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Empty copyWith(void Function(Empty) updates) => super.copyWith((message) => updates(message as Empty)) as Empty;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Empty create() => Empty._();
  Empty createEmptyInstance() => create();
  static $pb.PbList<Empty> createRepeated() => $pb.PbList<Empty>();
  @$core.pragma('dart2js:noInline')
  static Empty getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Empty>(create);
  static Empty? _defaultInstance;
}

class MessageGrpc extends $pb.GeneratedMessage {
  factory MessageGrpc({
    $core.String? message,
    $core.String? userName,
    $core.Iterable<FileMessageGrpc>? files,
    $core.String? receiverUserName,
    $core.String? groupName,
  }) {
    final $result = create();
    if (message != null) {
      $result.message = message;
    }
    if (userName != null) {
      $result.userName = userName;
    }
    if (files != null) {
      $result.files.addAll(files);
    }
    if (receiverUserName != null) {
      $result.receiverUserName = receiverUserName;
    }
    if (groupName != null) {
      $result.groupName = groupName;
    }
    return $result;
  }
  MessageGrpc._() : super();
  factory MessageGrpc.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory MessageGrpc.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'MessageGrpc', package: const $pb.PackageName(_omitMessageNames ? '' : 'greet'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'message')
    ..aOS(2, _omitFieldNames ? '' : 'userName')
    ..pc<FileMessageGrpc>(3, _omitFieldNames ? '' : 'files', $pb.PbFieldType.PM, subBuilder: FileMessageGrpc.create)
    ..aOS(4, _omitFieldNames ? '' : 'receiverUserName')
    ..aOS(5, _omitFieldNames ? '' : 'groupName')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  MessageGrpc clone() => MessageGrpc()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  MessageGrpc copyWith(void Function(MessageGrpc) updates) => super.copyWith((message) => updates(message as MessageGrpc)) as MessageGrpc;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static MessageGrpc create() => MessageGrpc._();
  MessageGrpc createEmptyInstance() => create();
  static $pb.PbList<MessageGrpc> createRepeated() => $pb.PbList<MessageGrpc>();
  @$core.pragma('dart2js:noInline')
  static MessageGrpc getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<MessageGrpc>(create);
  static MessageGrpc? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get message => $_getSZ(0);
  @$pb.TagNumber(1)
  set message($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasMessage() => $_has(0);
  @$pb.TagNumber(1)
  void clearMessage() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get userName => $_getSZ(1);
  @$pb.TagNumber(2)
  set userName($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasUserName() => $_has(1);
  @$pb.TagNumber(2)
  void clearUserName() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<FileMessageGrpc> get files => $_getList(2);

  @$pb.TagNumber(4)
  $core.String get receiverUserName => $_getSZ(3);
  @$pb.TagNumber(4)
  set receiverUserName($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasReceiverUserName() => $_has(3);
  @$pb.TagNumber(4)
  void clearReceiverUserName() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get groupName => $_getSZ(4);
  @$pb.TagNumber(5)
  set groupName($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasGroupName() => $_has(4);
  @$pb.TagNumber(5)
  void clearGroupName() => clearField(5);
}

class FileMessageGrpc extends $pb.GeneratedMessage {
  factory FileMessageGrpc({
    $core.List<$core.int>? content,
    $core.Map<$core.String, $core.String>? metaData,
    $core.String? fileName,
    $core.String? mainBucketIdentifier,
    $core.String? temporaryBucketIdentifier,
  }) {
    final $result = create();
    if (content != null) {
      $result.content = content;
    }
    if (metaData != null) {
      $result.metaData.addAll(metaData);
    }
    if (fileName != null) {
      $result.fileName = fileName;
    }
    if (mainBucketIdentifier != null) {
      $result.mainBucketIdentifier = mainBucketIdentifier;
    }
    if (temporaryBucketIdentifier != null) {
      $result.temporaryBucketIdentifier = temporaryBucketIdentifier;
    }
    return $result;
  }
  FileMessageGrpc._() : super();
  factory FileMessageGrpc.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory FileMessageGrpc.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'FileMessageGrpc', package: const $pb.PackageName(_omitMessageNames ? '' : 'greet'), createEmptyInstance: create)
    ..a<$core.List<$core.int>>(1, _omitFieldNames ? '' : 'content', $pb.PbFieldType.OY)
    ..m<$core.String, $core.String>(2, _omitFieldNames ? '' : 'metaData', entryClassName: 'FileMessageGrpc.MetaDataEntry', keyFieldType: $pb.PbFieldType.OS, valueFieldType: $pb.PbFieldType.OS, packageName: const $pb.PackageName('greet'))
    ..aOS(3, _omitFieldNames ? '' : 'fileName')
    ..aOS(4, _omitFieldNames ? '' : 'mainBucketIdentifier')
    ..aOS(5, _omitFieldNames ? '' : 'temporaryBucketIdentifier')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  FileMessageGrpc clone() => FileMessageGrpc()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  FileMessageGrpc copyWith(void Function(FileMessageGrpc) updates) => super.copyWith((message) => updates(message as FileMessageGrpc)) as FileMessageGrpc;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static FileMessageGrpc create() => FileMessageGrpc._();
  FileMessageGrpc createEmptyInstance() => create();
  static $pb.PbList<FileMessageGrpc> createRepeated() => $pb.PbList<FileMessageGrpc>();
  @$core.pragma('dart2js:noInline')
  static FileMessageGrpc getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<FileMessageGrpc>(create);
  static FileMessageGrpc? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.int> get content => $_getN(0);
  @$pb.TagNumber(1)
  set content($core.List<$core.int> v) { $_setBytes(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasContent() => $_has(0);
  @$pb.TagNumber(1)
  void clearContent() => clearField(1);

  @$pb.TagNumber(2)
  $core.Map<$core.String, $core.String> get metaData => $_getMap(1);

  @$pb.TagNumber(3)
  $core.String get fileName => $_getSZ(2);
  @$pb.TagNumber(3)
  set fileName($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasFileName() => $_has(2);
  @$pb.TagNumber(3)
  void clearFileName() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get mainBucketIdentifier => $_getSZ(3);
  @$pb.TagNumber(4)
  set mainBucketIdentifier($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasMainBucketIdentifier() => $_has(3);
  @$pb.TagNumber(4)
  void clearMainBucketIdentifier() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get temporaryBucketIdentifier => $_getSZ(4);
  @$pb.TagNumber(5)
  set temporaryBucketIdentifier($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasTemporaryBucketIdentifier() => $_has(4);
  @$pb.TagNumber(5)
  void clearTemporaryBucketIdentifier() => clearField(5);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');
