import 'package:flutter/material.dart';

import '../../styles/home_style.dart';
import 'beaver_drawer.dart';
class BeaverScaffold extends StatelessWidget {
  final Widget? body;
  final String title;
  const BeaverScaffold ({
    super.key,
    required this.body,
    required this.title,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
        backgroundColor: const HomeStyles().MainColor(),
    centerTitle: true,
    title: Text(
     title,
    style: const HomeStyles().getMainTextStyle()),
    ),
    drawer: BeaverDrawer(),
    body:  body
    );
  }
}
