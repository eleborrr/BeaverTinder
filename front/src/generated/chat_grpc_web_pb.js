/**
 * @fileoverview gRPC-Web generated client stub for greet
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');


var google_protobuf_empty_pb = require('google-protobuf/google/protobuf/empty_pb')
const proto = {};
proto.greet = require('./chat_pb');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.greet.ChatClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

  /**
   * @private @const {?Object} The credentials to be used to connect
   *    to the server
   */
  this.credentials_ = credentials;

  /**
   * @private @const {?Object} Options for the client
   */
  this.options_ = options;
};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.greet.ChatPromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

  /**
   * @private @const {?Object} The credentials to be used to connect
   *    to the server
   */
  this.credentials_ = credentials;

  /**
   * @private @const {?Object} Options for the client
   */
  this.options_ = options;
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.google.protobuf.Empty,
 *   !proto.greet.JoinResponse>}
 */
const methodInfo_Chat_JoinChat = new grpc.web.AbstractClientBase.MethodInfo(
  proto.greet.JoinResponse,
  /** @param {!proto.google.protobuf.Empty} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.greet.JoinResponse.deserializeBinary
);


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.greet.JoinResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.greet.JoinResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatClient.prototype.joinChat =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/greet.Chat/JoinChat',
      request,
      metadata || {},
      methodInfo_Chat_JoinChat,
      callback);
};


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.greet.JoinResponse>}
 *     A native promise that resolves to the response
 */
proto.greet.ChatPromiseClient.prototype.joinChat =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/greet.Chat/JoinChat',
      request,
      metadata || {},
      methodInfo_Chat_JoinChat);
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.greet.Msg,
 *   !proto.google.protobuf.Empty>}
 */
const methodInfo_Chat_SendMsg = new grpc.web.AbstractClientBase.MethodInfo(
  google_protobuf_empty_pb.Empty,
  /** @param {!proto.greet.Msg} request */
  function(request) {
    return request.serializeBinary();
  },
  google_protobuf_empty_pb.Empty.deserializeBinary
);


/**
 * @param {!proto.greet.Msg} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.google.protobuf.Empty)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.google.protobuf.Empty>|undefined}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatClient.prototype.sendMsg =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/greet.Chat/SendMsg',
      request,
      metadata || {},
      methodInfo_Chat_SendMsg,
      callback);
};


/**
 * @param {!proto.greet.Msg} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.google.protobuf.Empty>}
 *     A native promise that resolves to the response
 */
proto.greet.ChatPromiseClient.prototype.sendMsg =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/greet.Chat/SendMsg',
      request,
      metadata || {},
      methodInfo_Chat_SendMsg);
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.google.protobuf.Empty,
 *   !proto.greet.Msg>}
 */
const methodInfo_Chat_ReceiveMsg = new grpc.web.AbstractClientBase.MethodInfo(
  proto.greet.Msg,
  /** @param {!proto.google.protobuf.Empty} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.greet.Msg.deserializeBinary
);


/**
 * @param {!proto.google.protobuf.Empty} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.greet.Msg>}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatClient.prototype.receiveMsg =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/greet.Chat/ReceiveMsg',
      request,
      metadata || {},
      methodInfo_Chat_ReceiveMsg);
};


/**
 * @param {!proto.google.protobuf.Empty} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.greet.Msg>}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatPromiseClient.prototype.receiveMsg =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/greet.Chat/ReceiveMsg',
      request,
      metadata || {},
      methodInfo_Chat_ReceiveMsg);
};


module.exports = proto.greet;

