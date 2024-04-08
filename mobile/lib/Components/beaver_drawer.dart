import 'package:flutter/material.dart';
import 'package:mobile/styles/home_style.dart';

class BeaverDrawer extends StatelessWidget {
  const BeaverDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      backgroundColor: Colors.white,
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          DrawerHeader(
            decoration: BoxDecoration(
              color: const HomeStyles().MainColor(),
            ),
            child: const Text(
              'Menu',
              style: TextStyle(color: Colors.white, fontSize: 24),
            ),
          ),
          _buildListTile(
            context,
            title: 'Home Page',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, '/home');
            },
          ),
          _buildListTile(
            context,
            title: 'Like Page',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, '/like');
            },
          ),
          _buildListTile(
            context,
            title: 'Chats',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, '/chats');
            },
          ),
        ],
      ),
    );
  }

  Widget _buildListTile(BuildContext context, {required String title, required Function() onTap}) {
    return ListTile(
      title: Text(title),
      onTap: onTap,
    );
  }
}
