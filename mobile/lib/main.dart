//import 'package:flutter/cupertino.dart';
import 'package:mobile/components/shared/beaver_auth_provider.dart';
import 'package:mobile/components/shared/beaver_splash_screen.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/services/account_service.dart';
import 'package:mobile/navigation/navigation.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:mobile/services/auth_service.dart';
import 'package:mobile/services/chat_service.dart';
import 'package:provider/provider.dart';

final getit = GetIt.instance;
const link = 'http://192.168.31.179:8080/graphql/';

void setup() {
  getit.registerSingleton<AuthProvider>(AuthProvider());
  final httpLink = HttpLink(link);
  final authLink = AuthLink(getToken: () => '${getit<AuthProvider>().jwtToken}');
  final finalLink = authLink.concat(httpLink);
  getit.registerSingleton<GraphQLClient>(
      GraphQLClient(
      link: finalLink,
      cache: GraphQLCache())
  );
  getit.registerSingleton<AccountServiceBase>(AccountService());

  getit.registerSingleton<AuthServiceBase>(AuthService());
  getit.registerSingleton<ChatServiceBase>(ChatService());
}

void main() {
  setup();
  runApp(
    ChangeNotifierProvider(
      create: (_) => getit<AuthProvider>(),
      child: MaterialApp(
        onGenerateRoute: buildRoutes,
        home: BeaverSplashScreen(),
      ),
    ),
  );
  //runApp(const MyApp());
}

// class MyApp extends StatelessWidget {
//   const MyApp({super.key});
//
//   @override
//   Widget build(BuildContext context) {
//     return ChangeNotifierProvider(
//       create: (_) => AuthProvider(),
//       child: MaterialApp(
//         title: 'Beaver Tinder',
//         routes: {
//           '/login': (context) => LoginPage(),
//           '/register': (context) => RegisterPage(),
//           '/like': (context) => const LikePage(),
//           '/home': (context) => const HomePage(),
//           '/chats': (context) => const ChatsPage(),
//           '/chat': (context) {
//             final args = ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
//             final chatId = args['id'];
//             return ChatPage(chatId: chatId);
//           },
//           '/shops': (context) => SubscriptionPage(),
//           '/profile': (context) => const ProfilePage(),
//
//         },
//         home: BeaverSplashScreen(), // Используйте MainPage как вашу домашнюю страницу
//       ),
//     );
//   }
// }
