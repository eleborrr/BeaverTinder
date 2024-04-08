import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_auth_provider.dart';
import 'package:mobile/Components/shared/beaver_splash_screen.dart';
import 'package:mobile/Pages/chats_page.dart';
import 'package:mobile/Pages/home_page.dart';
import 'package:mobile/Pages/subscription_page.dart';
import 'Components/shared/beaver_drawer.dart';
import 'Pages/chat_for_two.dart';
import 'Pages/like_page.dart';
import 'Pages/login_page.dart';
import 'Pages/profile_page.dart';
import 'Pages/register_page.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (_) => AuthProvider(),
      child: MaterialApp(
        title: 'Beaver Tinder',
        routes: {
          '/login': (context) => LoginPage(),
          '/register': (context) => RegisterPage(),
          '/like': (context) => const LikePage(),
          '/home': (context) => const HomePage(),
          '/chats': (context) => const ChatsPage(),
          '/chat': (context) {
            final args = ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
            final chatId = args['id'];
            return ChatPage(chatId: chatId);
          },
          '/shops': (context) => SubscriptionPage(),
          '/profile': (context) => const ProfilePage(),

        },
        home: BeaverSplashScreen(), // Используйте MainPage как вашу домашнюю страницу
      ),
    );
  }
}