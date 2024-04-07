import 'package:flutter/material.dart';
import 'package:mobile/Pages/home_page.dart';
import 'Components/beaver_drawer.dart';
import 'Pages/like_page.dart';
import 'Pages/login_page.dart';
import 'Pages/register_page.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Beaver Tinder',
      routes: {
        '/login': (context) => LoginPage(),
        '/register': (context) => RegisterPage(),
        '/like': (context) => const LikePage(),
        '/home': (context) => const HomePage()
      },
      home: const MainPage(), // Используйте MainPage как вашу домашнюю страницу
    );
  }
}

class MainPage extends StatelessWidget {
  const MainPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: HomePage(), // Ваша домашняя страница по умолчанию
    );
  }
}
