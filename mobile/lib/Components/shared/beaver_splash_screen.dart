import 'dart:async';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

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
    final authProvider = Provider.of<AuthProvider>(context, listen: false);
    final token = authProvider.jwtToken;
    if (token != null) {
      Navigator.pushReplacementNamed(context, '/home');
    } else {
      Navigator.pushReplacementNamed(context, '/login');
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