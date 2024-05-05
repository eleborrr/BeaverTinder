import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:graphql_flutter/graphql_flutter.dart';

import '../Components/shared/beaver_button.dart';
import '../Components/shared/beaver_drawer.dart';
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
  var selectedGender = "Male";

  final HttpLink httpLink = HttpLink(
    'http://192.168.0.111:5292/graphql/', // Замените на URL вашего GraphQL сервера
  );

  void registerUser(BuildContext context) async {
    final String lastname = lastnameController.text;
    final String firstname = firstnameController.text;
    final String username = usernameController.text;
    final String email = emailController.text;
    final String birthdate = birthdateController.text;
    final String password = passwordController.text;
    final String cPassword = cPasswordController.text;
    final String about = aboutController.text;

    final ValueNotifier<GraphQLClient> clientNotifier = ValueNotifier(
      GraphQLClient(
        link: httpLink,
        cache: GraphQLCache(),
      ),
    );

    final MutationOptions options = MutationOptions(
      document: gql('''
      mutation {
        register(model: {
          lastName: "$lastname",
          firstName: "$firstname",
          userName: "$username",
          email: "$email",
          dateOfBirth: "$birthdate",
          password: "$password",
          confirmPassword: "$cPassword",
          gender: "$selectedGender",
          about: "$about",
          latitude: 0,
          longitude: 0
        }) {
          successful
          message
          statusCode
        }
      }
    '''),
    );

    final QueryResult result = await clientNotifier.value.mutate(options);

    // Обработка ответа от сервера
    if (result.hasException) {
      // Обработка ошибки
    } else {
      // Обработка успешного ответа
      // Например, перенаправление на другой экран
      Navigator.pushNamed(context, '/login');
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
                  Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: <Widget>[
                        GestureDetector(
                          onTap: () {
                            selectedGender = "Male";
                          },
                          child: Image.asset("lib/images/male.png", width: 100, height: 100,),
                        ),
                        GestureDetector(
                          onTap: () {
                            selectedGender = "Woman";
                          },
                          child: Image.asset("lib/images/woman.png", width: 100, height: 100,),
                        ),
                      ]
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      controller: aboutController,
                      hintText: "Tell about yourself",
                      obscureText: false
                  ),

                  const SizedBox(height: 10.0),

                  DatePicker

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
