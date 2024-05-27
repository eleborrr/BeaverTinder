import 'dart:async';

import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/dto/chat_admin/chat_message_admin.dart';
import 'package:mobile/dto/chat_admin/room_data.dart';
import 'package:mobile/generated/chat.pbgrpc.dart';
import 'package:flutter/material.dart';
import 'package:mobile/main.dart';
import 'package:grpc/grpc.dart' as $grpc;
import 'package:mobile/services/dio_client.dart';

class ChatAdminPage extends StatefulWidget {

  ChatAdminPage({super.key});

  @override
  State<ChatAdminPage> createState() => ChatAdminPageState();
}

class ChatAdminPageState extends State<ChatAdminPage> {

  late StreamController<MessageGrpc> _streamController = StreamController<MessageGrpc>();
  late List<ChatMessageAdmin> _messages = [];
  late Stream<MessageGrpc> stream = const Stream.empty();
  final _dio = getit<DioClient>();
  late String userName;
  late RoomData roomData;
  final TextEditingController _messageController = TextEditingController();
  final  _chatAdminClient = ChatClient(getit<$grpc.ClientChannel>());

  @override
  void initState() {
    super.initState();
    fetchData(this);
  }

  void fetchData(context) async {
    roomData = (await _dio.getAdminRoomData())!;
    _messages = (await _dio.getAdminChatHistory())!;
    var request = JoinRequest();
    request.roomName = roomData.roomName;
    request.userName = roomData.senderName;
    _chatAdminClient.connectToRoom(request).listen((data) {
      setState(() {
        _messages.add(ChatMessageAdmin(
          senderName: data.userName,
          content: data.message,
        ));
      });
    });
    setState(() {}); // Добавьте эту строку для обновления списка сообщений
  }


  Future<void> _sendMessage() async {
    var mess = MessageGrpc();
    mess.message = _messageController.text;
    _messageController.text = "";
    mess.userName = roomData.senderName;
    mess.groupName = roomData.roomName;
    mess.receiverUserName = roomData.receiverName;
    await _chatAdminClient.sendMessage(mess);
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: 'Chat with Admin',
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: _messages.length,
              itemBuilder: (context, index) {
                final message = _messages[index];
                return Align(
                  alignment: message.senderName != 'Admin' ? Alignment.centerRight : Alignment.centerLeft,
                  child: Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                    child: Container(
                      decoration: BoxDecoration(
                        color: message.senderName != 'Admin' ? Colors.blue : Colors.grey,
                        borderRadius: BorderRadius.circular(8),
                      ),
                      padding: const EdgeInsets.all(8),
                      child: Text(
                        message.content,
                        style: const TextStyle(color: Colors.white),
                      ),
                    ),
                  ),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _messageController,
                    decoration: const InputDecoration(
                      hintText: 'Type a message...',
                    ),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.send),
                  onPressed: _sendMessage,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  @override
  void dispose() {
    _messageController.dispose();
    super.dispose();
  }
}
