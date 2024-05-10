import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobile/Components/server/auth_service.dart';
import 'package:mobile/Components/server/dto/register/register_request_dto.dart';

import '../Components/server/UseCase.dart';
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
  var selectedGender = "Male";

  void registerUser(BuildContext context) async {
    final String lastname = lastnameController.text;
    final String firstname = firstnameController.text;
    final String username = usernameController.text;
    final String email = emailController.text;
    final String birthdate = birthdateController.text;
    final String password = passwordController.text;
    final String cPassword = cPasswordController.text;
    final String about = aboutController.text;
    final double latitude = 0;
    final double longitude = 0;

    final UseCase useCase = UseCase(dataService: AuthService());

    final registerResponse = await useCase.dataService.register(RegisterRequestDto(
        lastname,
        firstname,
        username,
        email,
        password,
        cPassword,
        birthdate,
        latitude,
        longitude,
        selectedGender,
        about));
  }



  goToSignIn(BuildContext context) {
    Navigator.pushReplacementNamed(
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
