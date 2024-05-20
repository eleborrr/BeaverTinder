import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/dto/chats/allchat_response_dto.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/chat_service.dart';

class ChatsPage extends StatefulWidget {
  const ChatsPage({super.key});

  @override
  _ChatsPageState createState() => _ChatsPageState();
}

class _ChatsPageState extends State<ChatsPage> {
  final ChatServiceBase _chatService = getit<ChatServiceBase>();
  final TextEditingController _searchInputController = TextEditingController();
  bool _searchNone = true;
  List<AllChatResponseDto> _chats = [];
  List<AllChatResponseDto> _chatsView = [];
  bool _networkError = false;

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  Future<void> fetchData() async {
    final chatResponse = await _chatService.getAllChatAsync();
    if (chatResponse.success != null) {
      setState(() {
        _chats = chatResponse.success!;
        _chatsView = _chats;
      });
    } else {
      setState(() {
        _networkError = true;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: 'Chats',
      appBarAction: IconButton(
        icon: Icon(_searchNone ? Icons.search : Icons.close),
        onPressed: () {
          setState(() {
            _searchNone = !_searchNone;
            if (_searchNone) {
              _chatsView = _chats;
              _searchInputController.clear();
            }
          });
        },
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            if (_networkError)
              const Text('Connection error, please reload the page'),
            if (!_networkError && !_searchNone)
              TextField(
                controller: _searchInputController,
                decoration: const InputDecoration(
                  hintText: 'Search...',
                ),
                onChanged: (_) {
                  setState(() {
                    _chatsView = _chats.where((chat) {
                      final query = _searchInputController.text.toLowerCase();
                      return chat.firstName.toLowerCase().contains(query) ||
                          chat.lastName.toLowerCase().contains(query);
                    }).toList();
                  });
                },
              ),
            ListView.builder(
              shrinkWrap: true,
              itemCount: _chatsView.length,
              itemBuilder: (context, index) {
                final chat = _chatsView[index];
                return ListTile(
                  onTap: () => Navigator.pushNamed(
                    context,
                    '/chat',
                    arguments: {'id': chat.userName},
                  ),
                  leading: CircleAvatar(
                    backgroundImage: NetworkImage(chat.image),
                    onBackgroundImageError: (_, __) => Icon(Icons.person),
                  ),
                  title: Text('${chat.firstName} ${chat.lastName}'),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
