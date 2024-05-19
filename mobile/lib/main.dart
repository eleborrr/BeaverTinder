//import 'package:flutter/cupertino.dart';
import 'package:mobile/components/shared/beaver_auth_provider.dart';
import 'package:mobile/components/shared/beaver_splash_screen.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/services/account_service.dart';
import 'package:mobile/navigation/navigation.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:mobile/services/auth_service.dart';
import 'package:mobile/services/subscription_service.dart';
import 'package:provider/provider.dart';

final getit = GetIt.instance;
final authProvider = AuthProvider();
const link = 'http://192.168.0.10:8080/graphql/';

void setup() {
  getit.registerSingleton<GraphQLClient>(GraphQLClient(
      link: AuthLink(getToken: () => 'Bearer token')
          .concat(HttpLink(link)),
      cache: GraphQLCache()));
  getit.registerSingleton<AccountServiceBase>(AccountService());
  getit.registerSingleton<AuthProvider>(AuthProvider());
  getit.registerSingleton<AuthServiceBase>(AuthService());
  getit.registerSingleton<SubscriptionServiceBase>(SubscriptionService());
}

void main() {
  setup();
  runApp(
    ChangeNotifierProvider(
      create: (_) => authProvider,
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
