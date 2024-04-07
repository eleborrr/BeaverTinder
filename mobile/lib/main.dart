import 'package:flutter/material.dart';
import 'Components/beaver_drawer.dart';
import 'Pages/like_page.dart';
import 'Pages/login_page.dart';
import 'Pages/register_page.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Beaver Tinder',
      routes: {
        '/login': (context) => LoginPage(),
        '/register': (context) => RegisterPage(),
        '/like': (context) => LikePage(),
      },
      home: MainPage(), // Используйте MainPage как вашу домашнюю страницу
    );
  }
}

class MainPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: LikePage(), // Ваша домашняя страница по умолчанию
    );
  }
}
