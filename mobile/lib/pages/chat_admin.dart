import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/generated/chat.pbgrpc.dart';
import 'package:flutter/material.dart';
import 'package:mobile/main.dart';
import 'package:grpc/grpc.dart' as $grpc;

class ChatAdminPage extends StatefulWidget {
  final String userName;

  ChatAdminPage({super.key, required this.userName});

  @override
  State<ChatAdminPage> createState() => ChatAdminPageState();
}

class ChatAdminPageState extends State<ChatAdminPage> {
  final List<Map<String, String>> _messages = [];
  late String userName;
  late String roomId;
  final TextEditingController _messageController = TextEditingController();
  final  _chatAdminService = ChatClient(getit<$grpc.ClientChannel>());

  @override
  void initState() {
    super.initState();
    fetchData(this);
  }

  void fetchData(context) async {
      var request = JoinRequest();
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
    super.dispose();
  }
}
