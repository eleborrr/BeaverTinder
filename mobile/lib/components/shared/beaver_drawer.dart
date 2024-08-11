import 'package:flutter/material.dart';
import 'package:mobile/navigation/navigation_routes.dart';
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
              Navigator.pushNamed(context, NavigationRoutes.home);
            },
          ),
          _buildListTile(
            context,
            title: 'Like Page',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.like);
            },
          ),
          _buildListTile(
            context,
            title: 'Chats',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.chats);
            },
          ),
          _buildListTile(
            context,
            title: 'Shops Page',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.shops);
            },
          ),
          _buildListTile(
            context,
            title: 'Profile Page',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.profile);
            },
          ),
          _buildListTile(
            context,
            title: 'Fun fact',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.funFact);
            },

          ),
          _buildListTile(
            context,
            title: 'Support chat',
            onTap: () {
              Navigator.of(context).pop();
              Navigator.pushNamed(context, NavigationRoutes.supportChat);
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
