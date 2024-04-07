import 'package:flutter/material.dart';
import 'package:mobile/Pages/home_page.dart';
import 'package:mobile/Pages/login_page.dart';

import 'Components/Navigation.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Beaver Tinder',
      routes: {
        '/' : (context) =>  
            const HomePage(),
      },

    );
  }
}

