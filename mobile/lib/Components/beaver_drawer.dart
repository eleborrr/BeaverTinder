import 'package:flutter/material.dart';

class BeaverDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          const DrawerHeader(
            decoration: BoxDecoration(
              color: Colors.blue,
            ),
            child: Text(
              'Menu',
              style: TextStyle(color: Colors.white, fontSize: 24),
            ),
          ),
          _buildListTile(
            context,
            title: 'Like Page',
            onTap: () {
              Navigator.of(context).pop(); // Закрываем боковое меню
              Navigator.pushNamed(context, '/like');
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
