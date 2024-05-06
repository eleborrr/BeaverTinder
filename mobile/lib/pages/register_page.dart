import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:graphql_flutter/graphql_flutter.dart';

import '../Components/shared/beaver_button.dart';
import '../Components/shared/beaver_textfield.dart';

class RegisterPage extends StatelessWidget {

  final lastnameController = TextEditingController();
  final firstnameController = TextEditingController();
  final usernameController = TextEditingController();
  final emailController = TextEditingController();
  final birthdateController = TextEditingController();
  final passwordController = TextEditingController();
  final cPasswordController = TextEditingController();
  final genderController = TextEditingController();
  final aboutController = TextEditingController();
  final geolocationController = TextEditingController();

  final HttpLink httpLink = HttpLink(
    'YOUR_GRAPHQL_ENDPOINT', // Замените на URL вашего GraphQL сервера
  );

  void registerUser(BuildContext context) async {
    final String lastname = lastnameController.text;
    final String firstname = firstnameController.text;
    final String username = usernameController.text;
    final String email = emailController.text;
    final String birthdate = birthdateController.text;
    final String password = passwordController.text;
    final String cPassword = cPasswordController.text;
    final String gender = genderController.text;
    final String about = aboutController.text;
    final String geolocation = geolocationController.text;

    final ValueNotifier<GraphQLClient> clientNotifier = ValueNotifier(
      GraphQLClient(
        link: httpLink,
        cache: GraphQLCache(),
      ),
    );

    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation Register(\$input: RegisterInput!) {
        register(input: \$input) {
          // Поля, которые вам нужны из ответа сервера
        }
      }
    '''),
      variables: {
        'input': {
          'lastname': lastname,
          'firstname': firstname,
          'username': username,
          'email': email,
          'birthdate': birthdate,
          'password': password,
          'cPassword': cPassword,
          'gender': gender,
          'about': about,
          'geolocation': geolocation,
        },
      },
    );

    final QueryResult result = await clientNotifier.value.mutate(options);

    // Обработка ответа от сервера
    if (result.hasException) {
      // Обработка ошибки
    } else {
      // Обработка успешного ответа
      // Например, перенаправление на другой экран
      Navigator.pushNamed(context, '/home');
    }
  }


  goToSignIn(BuildContext context) {
    Navigator.pushNamed(
        context, '/login'
    );
  }

  RegisterPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: const BoxDecoration(
            image: DecorationImage(
              image: AssetImage("lib/images/pageheader.png"),
              fit: BoxFit.cover,
            )
        ),
        child: SafeArea(
          child: Center(
              child: ListView(
                children: [
                  Image.asset("lib/images/logo.png"),

                  BeaverTextField(
                    controller: lastnameController,
                    hintText: "Last name",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    controller: firstnameController,
                    hintText: "First name",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    controller: usernameController,
                    hintText: "Username",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    controller: emailController,
                    hintText: "someEmail@gmail.com",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    controller: birthdateController,
                    hintText: "Birth date : dd.mm.yyyy",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: passwordController,
                      hintText: "Password",
                      obscureText: true
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: cPasswordController,
                      hintText: "Confirm password",
                      obscureText: true
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: genderController,
                      hintText: "Gender Man / Woman",
                      obscureText: false
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: aboutController,
                      hintText: "Tell about yourself",
                      obscureText: false
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: geolocationController,
                      hintText: "Geolocation",
                      obscureText: false
                  ),

                  const SizedBox(height: 10.0),

                  BeaverButton(
                    buttonText: "Sign Up",
                    onTap: () => registerUser(context),
                  ),

                  const SizedBox(height: 10.0),

                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 25.0),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        GestureDetector(
                          onTap: () => goToSignIn(context),
                          child: Text(
                            "Already have account?",
                            style: TextStyle(color: Colors.grey[600]),
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              )
          ),
        ),
      ),

    );
  }


}
