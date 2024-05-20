import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/chat_for_two_service.dart';
import 'package:mobile/services/signalR_service.dart';
import 'package:signalr_core/signalr_core.dart';

class ChatPage extends StatefulWidget {
  final String userName;

  ChatPage({super.key, required this.userName});

  @override
  State<ChatPage> createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final List<Map<String, String>> _messages = [];
  late SignalRService _signalRService;
  late String userName;
  late String roomId;
  final TextEditingController _messageController = TextEditingController();
  final ChatForTwoServiceBase _chatService = getit<ChatForTwoServiceBase>();

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  void fetchData() {
    _chatService.getChatAsync(widget.userName).then((chatForTwoResponse) {
      if (chatForTwoResponse.success != null) {
        setState(() {
          roomId = chatForTwoResponse.success!.roomName;
          userName = chatForTwoResponse.success!.senderName;
        });

        _signalRService = SignalRService('http://192.168.31.179:4040');
        _signalRService.hubConnection.on('ReceivePrivateMessage', _handleReceiveMessage);

        _signalRService.connect().then((_) {
          _signalRService.getMessages(roomId);
        }).catchError((error) {
          print('Error connecting to SignalR: $error');
        });
      } else {
        print('Error fetching chat data');
      }
    }).catchError((error) {
      print('Error fetching chat data: $error');
    });
  }

  void _handleReceiveMessage(List<Object?>? parameters) {
    if (parameters != null && parameters.length == 3) {
      final user = parameters[0] as String;
      final message = parameters[1] as String;
      setState(() {
        _messages.add({'text': message, 'sender': user != widget.userName ? 'me' : 'other'});
      });
    }
  }

  Future<void> _sendMessage() async {
    if (_messageController.text.isNotEmpty) {
      await _signalRService.sendMessage(userName, _messageController.text, [], widget.userName, roomId);
      _messageController.clear();
    }
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: 'Chat with ${widget.userName}',
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: _messages.length,
              itemBuilder: (context, index) {
                final message = _messages[index];
                return Align(
                  alignment: message['sender'] == 'me' ? Alignment.centerRight : Alignment.centerLeft,
                  child: Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                    child: Container(
                      decoration: BoxDecoration(
                        color: message['sender'] == 'me' ? Colors.blue : Colors.grey,
                        borderRadius: BorderRadius.circular(8),
                      ),
                      padding: const EdgeInsets.all(8),
                      child: Text(
                        message['text']!,
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
    _signalRService.disconnect();
    super.dispose();
  }
}
