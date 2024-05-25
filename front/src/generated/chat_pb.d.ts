import * as jspb from 'google-protobuf'

import * as google_protobuf_empty_pb from 'google-protobuf/google/protobuf/empty_pb';


export class JoinResponse extends jspb.Message {
  getError(): number;
  setError(value: number): JoinResponse;

  getMsg(): string;
  setMsg(value: string): JoinResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): JoinResponse.AsObject;
  static toObject(includeInstance: boolean, msg: JoinResponse): JoinResponse.AsObject;
  static serializeBinaryToWriter(message: JoinResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): JoinResponse;
  static deserializeBinaryFromReader(message: JoinResponse, reader: jspb.BinaryReader): JoinResponse;
}

export namespace JoinResponse {
  export type AsObject = {
    error: number,
    msg: string,
  }
}

export class Msg extends jspb.Message {
  getContent(): string;
  setContent(value: string): Msg;

  getFrom(): string;
  setFrom(value: string): Msg;

  getTo(): string;
  setTo(value: string): Msg;

  getRoom(): string;
  setRoom(value: string): Msg;

  getTime(): string;
  setTime(value: string): Msg;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Msg.AsObject;
  static toObject(includeInstance: boolean, msg: Msg): Msg.AsObject;
  static serializeBinaryToWriter(message: Msg, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Msg;
  static deserializeBinaryFromReader(message: Msg, reader: jspb.BinaryReader): Msg;
}

export namespace Msg {
  export type AsObject = {
    content: string,
    from: string,
    to: string,
    room: string,
    time: string,
  }
}

