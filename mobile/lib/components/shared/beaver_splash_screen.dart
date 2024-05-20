import 'dart:async';
import 'package:flutter/material.dart';
import 'package:mobile/main.dart';
import '../../navigation/navigation_routes.dart';
import 'beaver_auth_provider.dart';

class BeaverSplashScreen extends StatefulWidget {
  @override
  _BeaverSplashScreenState createState() => _BeaverSplashScreenState();
}

class _BeaverSplashScreenState extends State<BeaverSplashScreen> {
  @override
  void initState() {
    super.initState();
    Timer(
        const Duration(seconds: 3),
            () {
                  _checkTokenAndNavigate();
                });
  }

  void _checkTokenAndNavigate() {
    final authProvider = getit<AuthProvider>();
    final token = authProvider.jwtToken;
    if (token != null) {
      Navigator.of(context)
        ..pop()
        ..pushNamed(NavigationRoutes.home);
    } else {
      Navigator.of(context)
        ..pop()
        ..pushNamed(NavigationRoutes.login);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Center(
        child: Image.asset('lib/images/loading.png'),
      ),
    );
  }
}