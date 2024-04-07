import 'package:flutter/material.dart';
import 'package:mobile/Components/beaver_button.dart';
import 'package:mobile/Components/beaver_textfield.dart';
import 'package:provider/provider.dart';

import '../Components/shared/beaver_auth_provider.dart';

class LoginPage extends StatelessWidget {
  LoginPage({
    super.key});

  //text editing conroller

  final usernameController = TextEditingController();
  final passwordController = TextEditingController();

  // sign user in method
  signUserIn(BuildContext context) {
    var authProvider = Provider.of<AuthProvider>(context, listen: false);
    authProvider.setJwtToken("Bearer Not implemented");

    Navigator.pushReplacementNamed(
        context, '/home'
    );
  }

   goToSignUp(BuildContext context) {
    Navigator.pushNamed(
      context, '/register'
    );
  }

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
                  controller: usernameController,
                  hintText: "Username",
                  obscureText: false,
                ),

                const SizedBox(height: 30),

                BeaverTextField(
                  controller: passwordController,
                  hintText: "Password",
                  obscureText: true
                ),


                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 25.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      Text(
                        "Forgot Password?",
                        style: TextStyle(color: Colors.grey[600]),
                      ),
                    ],
                  ),
                ),

                const SizedBox(height: 25),

                BeaverButton(
                  buttonText: "Sign In",
                  onTap: () => signUserIn(context),
                ),

                const SizedBox(height: 25),

                BeaverButton(
                  buttonText: "Sign Up",
                  onTap: () => goToSignUp(context),
                )
              ],
            )
          ),
        ),
      ),

    );
  }

}