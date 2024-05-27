//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use joinRequestDescriptor instead')
const JoinRequest$json = {
  '1': 'JoinRequest',
  '2': [
    {'1': 'room_name', '3': 1, '4': 1, '5': 9, '10': 'roomName'},
    {'1': 'user_name', '3': 2, '4': 1, '5': 9, '10': 'userName'},
  ],
};

/// Descriptor for `JoinRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List joinRequestDescriptor = $convert.base64Decode(
    'CgtKb2luUmVxdWVzdBIbCglyb29tX25hbWUYASABKAlSCHJvb21OYW1lEhsKCXVzZXJfbmFtZR'
    'gCIAEoCVIIdXNlck5hbWU=');

@$core.Deprecated('Use clientMessageChatDescriptor instead')
const ClientMessageChat$json = {
  '1': 'ClientMessageChat',
  '2': [
    {'1': 'text', '3': 1, '4': 1, '5': 9, '10': 'text'},
    {'1': 'user_name', '3': 2, '4': 1, '5': 9, '10': 'userName'},
  ],
};

/// Descriptor for `ClientMessageChat`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List clientMessageChatDescriptor = $convert.base64Decode(
    'ChFDbGllbnRNZXNzYWdlQ2hhdBISCgR0ZXh0GAEgASgJUgR0ZXh0EhsKCXVzZXJfbmFtZRgCIA'
    'EoCVIIdXNlck5hbWU=');

@$core.Deprecated('Use emptyDescriptor instead')
const Empty$json = {
  '1': 'Empty',
};

/// Descriptor for `Empty`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List emptyDescriptor = $convert.base64Decode(
    'CgVFbXB0eQ==');

@$core.Deprecated('Use messageGrpcDescriptor instead')
const MessageGrpc$json = {
  '1': 'MessageGrpc',
  '2': [
    {'1': 'message', '3': 1, '4': 1, '5': 9, '10': 'message'},
    {'1': 'user_name', '3': 2, '4': 1, '5': 9, '10': 'userName'},
    {'1': 'files', '3': 3, '4': 3, '5': 11, '6': '.greet.FileMessageGrpc', '10': 'files'},
    {'1': 'receiver_user_name', '3': 4, '4': 1, '5': 9, '10': 'receiverUserName'},
    {'1': 'group_name', '3': 5, '4': 1, '5': 9, '10': 'groupName'},
  ],
};

/// Descriptor for `MessageGrpc`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List messageGrpcDescriptor = $convert.base64Decode(
    'CgtNZXNzYWdlR3JwYxIYCgdtZXNzYWdlGAEgASgJUgdtZXNzYWdlEhsKCXVzZXJfbmFtZRgCIA'
    'EoCVIIdXNlck5hbWUSLAoFZmlsZXMYAyADKAsyFi5ncmVldC5GaWxlTWVzc2FnZUdycGNSBWZp'
    'bGVzEiwKEnJlY2VpdmVyX3VzZXJfbmFtZRgEIAEoCVIQcmVjZWl2ZXJVc2VyTmFtZRIdCgpncm'
    '91cF9uYW1lGAUgASgJUglncm91cE5hbWU=');

@$core.Deprecated('Use fileMessageGrpcDescriptor instead')
const FileMessageGrpc$json = {
  '1': 'FileMessageGrpc',
  '2': [
    {'1': 'content', '3': 1, '4': 1, '5': 12, '10': 'content'},
    {'1': 'meta_data', '3': 2, '4': 3, '5': 11, '6': '.greet.FileMessageGrpc.MetaDataEntry', '10': 'metaData'},
    {'1': 'file_name', '3': 3, '4': 1, '5': 9, '10': 'fileName'},
    {'1': 'main_bucket_identifier', '3': 4, '4': 1, '5': 9, '10': 'mainBucketIdentifier'},
    {'1': 'temporary_bucket_identifier', '3': 5, '4': 1, '5': 9, '10': 'temporaryBucketIdentifier'},
  ],
  '3': [FileMessageGrpc_MetaDataEntry$json],
};

@$core.Deprecated('Use fileMessageGrpcDescriptor instead')
const FileMessageGrpc_MetaDataEntry$json = {
  '1': 'MetaDataEntry',
  '2': [
    {'1': 'key', '3': 1, '4': 1, '5': 9, '10': 'key'},
    {'1': 'value', '3': 2, '4': 1, '5': 9, '10': 'value'},
  ],
  '7': {'7': true},
};

/// Descriptor for `FileMessageGrpc`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List fileMessageGrpcDescriptor = $convert.base64Decode(
    'Cg9GaWxlTWVzc2FnZUdycGMSGAoHY29udGVudBgBIAEoDFIHY29udGVudBJBCgltZXRhX2RhdG'
    'EYAiADKAsyJC5ncmVldC5GaWxlTWVzc2FnZUdycGMuTWV0YURhdGFFbnRyeVIIbWV0YURhdGES'
    'GwoJZmlsZV9uYW1lGAMgASgJUghmaWxlTmFtZRI0ChZtYWluX2J1Y2tldF9pZGVudGlmaWVyGA'
    'QgASgJUhRtYWluQnVja2V0SWRlbnRpZmllchI+Cht0ZW1wb3JhcnlfYnVja2V0X2lkZW50aWZp'
    'ZXIYBSABKAlSGXRlbXBvcmFyeUJ1Y2tldElkZW50aWZpZXIaOwoNTWV0YURhdGFFbnRyeRIQCg'
    'NrZXkYASABKAlSA2tleRIUCgV2YWx1ZRgCIAEoCVIFdmFsdWU6AjgB');

