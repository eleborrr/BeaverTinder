/**
 * @fileoverview gRPC-Web generated client stub for greet
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');

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
 *   !proto.greet.JoinRequest,
 *   !proto.greet.MessageGrpc>}
 */
const methodInfo_Chat_ConnectToRoom = new grpc.web.AbstractClientBase.MethodInfo(
  proto.greet.MessageGrpc,
  /** @param {!proto.greet.JoinRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.greet.MessageGrpc.deserializeBinary
);


/**
 * @param {!proto.greet.JoinRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.greet.MessageGrpc>}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatClient.prototype.connectToRoom =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/greet.Chat/ConnectToRoom',
      request,
      metadata || {},
      methodInfo_Chat_ConnectToRoom);
};


/**
 * @param {!proto.greet.JoinRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.greet.MessageGrpc>}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatPromiseClient.prototype.connectToRoom =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/greet.Chat/ConnectToRoom',
      request,
      metadata || {},
      methodInfo_Chat_ConnectToRoom);
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.greet.MessageGrpc,
 *   !proto.greet.Empty>}
 */
const methodInfo_Chat_SendMessage = new grpc.web.AbstractClientBase.MethodInfo(
  proto.greet.Empty,
  /** @param {!proto.greet.MessageGrpc} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.greet.Empty.deserializeBinary
);


/**
 * @param {!proto.greet.MessageGrpc} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.greet.Empty)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.greet.Empty>|undefined}
 *     The XHR Node Readable Stream
 */
proto.greet.ChatClient.prototype.sendMessage =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/greet.Chat/SendMessage',
      request,
      metadata || {},
      methodInfo_Chat_SendMessage,
      callback);
};


/**
 * @param {!proto.greet.MessageGrpc} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.greet.Empty>}
 *     A native promise that resolves to the response
 */
proto.greet.ChatPromiseClient.prototype.sendMessage =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/greet.Chat/SendMessage',
      request,
      metadata || {},
      methodInfo_Chat_SendMessage);
};


module.exports = proto.greet;

