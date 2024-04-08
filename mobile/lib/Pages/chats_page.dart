import 'package:flutter/material.dart';
//import 'package:http/http.dart' as http; // Импорт для выполнения HTTP-запросов
import 'package:mobile/Components/shared/beaver_scaffold.dart'; // Импорт для работы с JSON

class ChatsPage extends StatefulWidget {
  const ChatsPage({super.key});

  @override
  _ChatsPageState createState() => _ChatsPageState();
}

class _ChatsPageState extends State<ChatsPage> {
  final String token = '';

  final TextEditingController _searchInputController = TextEditingController();
  bool _searchNone = true;
  List<dynamic> _chats = [];
  List<dynamic> _chatsView = [];
  final bool _networkError = false;

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  Future<void> fetchData() async {
    // if (token.isEmpty) {
    //   // Проверка наличия токена
    //   Navigator.pushNamed(context, '/login'); // Перенаправление на страницу входа
    // } else {
    //   try {
    //     final response = await http.get(
    //       Uri.parse('/im'), // URL для получения чатов
    //       headers: {
    //         'Authorization': 'Bearer $token',
    //         'Accept': 'application/json',
    //       },
    //     );
    //     if (response.statusCode == 200) {
    //       final List<dynamic> data = json.decode(response.body);
    //       setState(() {
    //         _chats = data;
    //       });
    //     } else {
    //       throw Exception('Failed to load chats');
    //     }
    //   } catch (e) {
    //     print(e);
    //     setState(() {
    //       _networkError = true;
    //     });
    //   }
    // }
    _chats = <dynamic>[
      {
        "userName": "Jon",
        "firstName": "FNAF",
        "image": "lib/images/profile.png",
        "lastName": "Doe"
      },
      {
        "userName": "Jon1",
        "firstName": "FNAF1",
        "image": "lib/images/profile.png",
        "lastName": "Doe1"
      },
      {
        "userName": "Jon2",
        "firstName": "FNAF2",
        "image": "lib/images/profile.png",
        "lastName": "Doe2"
      },
      {
        "userName": "Jon3",
        "firstName": "FNAF3",
        "image": "lib/images/profile.png",
        "lastName": "Doe3"
      },
      {
        "userName": "Jon4",
        "firstName": "FNAF4",
        "image": "lib/images/profile.png",
        "lastName": "Doe4"
      },
    ];
    _chatsView = _chats;
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
          });
        },
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            if (_networkError) // Проверка ошибки сети
              const Text('Connection error, please reload page'),
            if (!_networkError && !_searchNone) // Поле поиска
              TextField(
                controller: _searchInputController,
                decoration: const InputDecoration(
                  hintText: 'Search...',
                ),
                  onChanged: (_) {
                    setState(() {
                      _chatsView = _chats.where((element) =>
                      element["firstName"].contains(_searchInputController.text) ||
                          element["lastName"].contains(_searchInputController.text)
                      ).toList();
                    });
                  },
              ),
            ListView.builder(
              shrinkWrap: true,
              itemCount: _chatsView.length,
              itemBuilder: (context, index) {
                final chat = _chatsView[index];
                return ListTile(
                  onTap: () => Navigator.pushNamed(context, chat['userName']), // Навигация при выборе чата
                  leading: CircleAvatar(
                    child: Image.asset(chat['image']), // Изображение профиля
                  ),
                  title: Text('${chat['firstName']} ${chat['lastName']}'),
                );
              },
            ),
          ],
        ),
      )
    );
  }
}
