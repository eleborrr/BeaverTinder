import 'package:mobile/pages/subscription_page.dart';
import 'package:mobile/Pages/register_page.dart';
import 'package:mobile/Pages/profile_page.dart';
import 'package:mobile/pages/chat_for_two.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/pages/login_page.dart';
import 'package:mobile/pages/chats_page.dart';
import 'package:mobile/Pages/home_page.dart';
import 'package:mobile/pages/like_page.dart';
import 'package:mobile/block/block.dart';
import 'package:flutter/material.dart';
import 'navigation_routes.dart';

MaterialPageRoute? buildRoutes(RouteSettings settings) {

  return MaterialPageRoute(builder: (context) {
    final args = settings.arguments as Map<String, dynamic>?;
    final userName = args != null ? args['userName'] : "";
    return switch (settings.name) {
      NavigationRoutes.home =>  const HomePage(),
      NavigationRoutes.register => BlocProvider(
          create: (_) => LoadingBloc(),
          child: RegisterPage()
      ),
      NavigationRoutes.login => BlocProvider(
            create: (_) => LoadingBloc(),
            child: LoginPage()
      ),
      NavigationRoutes.like => BlocProvider(
            create: (_) => LoadingBloc(),
            child: LikePage()
      ),
      NavigationRoutes.chats => BlocProvider(
          create: (_) => LoadingBloc(),
          child: const ChatsPage()
      ),
      NavigationRoutes.chat => BlocProvider(
          create: (_) => LoadingBloc(),
          child: ChatPage(userName: userName)
      ),
      NavigationRoutes.shops => BlocProvider(
          create: (_) => LoadingBloc(),
          child: SubscriptionPage()
      ),
      NavigationRoutes.profile => BlocProvider(
        create: (_) => LoadingBloc(),
        child: const ProfilePage(),
      ),
      _ => throw Error()
    };
  });
}