import 'package:flutter/material.dart';
import 'package:mobile/dto/chat_for_two/chat_message_dto.dart';
import 'package:signalr_core/signalr_core.dart';



class SignalRService{
  late final HubConnection hubConnection;

  SignalRService(String link) {
    hubConnection = HubConnectionBuilder()
        .withUrl('$link/chatHub')
        .build();
  }

  Future<void> connect() async {
    await hubConnection.start();
  }

  Future<void> sendMessage(String user, String message, List? list, String receiver, String roomName) async {
    await hubConnection.invoke('SendPrivateMessage', args: [user, message, list, receiver, roomName]);
  }

  Future<void> disconnect() async {
    await hubConnection.stop();
  }

  void getMessages(String roomId) {
    hubConnection.invoke("GetGroupMessages", args: [roomId]);
  }
}