import 'package:flutter/material.dart';
import '../../styles/home_style.dart';
import '../beaver_drawer.dart';

class BeaverScaffold extends StatelessWidget {
  final Widget? body;
  final String title;
  final Widget? appBarAction;
  const BeaverScaffold ({
    super.key,
    required this.body,
    required this.title,
    this.appBarAction
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        actions: appBarAction == null ? null : [ appBarAction! ],
        foregroundColor: Colors.white,
        backgroundColor: const HomeStyles().MainColor(),
        centerTitle: true,
        title: Text(
         title,
        style: const HomeStyles().getMainTextStyle()),
      ),
      drawer: const BeaverDrawer(),
      body:  body
    );
  }
}
