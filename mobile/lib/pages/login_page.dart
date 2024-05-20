import 'package:flutter/material.dart';
import 'package:mobile/components/shared/beaver_button.dart';
import 'package:mobile/components/shared/beaver_textfield.dart';
import 'package:mobile/dto/login/login_request_dto.dart';
import 'package:mobile/main.dart';
import '../components/shared/beaver_auth_provider.dart';
import '../services/auth_service.dart';


class LoginPage extends StatelessWidget {
  LoginPage({
    super.key});

  final AuthServiceBase _authService = getit<AuthServiceBase>();

  //text editing conroller

  final usernameController = TextEditingController();
  final passwordController = TextEditingController();

  // sign user in method
  signUserIn(BuildContext context) async {
    var authProvider = getit<AuthProvider>();
    final String userName = usernameController.text;
    final String password = passwordController.text;

    final loginDto = LoginRequestDto(userName, password, true);

    final loginResponse = await _authService.loginAsync(loginDto);

    if(loginResponse.success == null || !(loginResponse.success!.successful))
    {
      return;
    }

    final jwtToken = loginResponse.success!.message;
    authProvider.setJwtToken('Bearer $jwtToken');
    Navigator.pushReplacementNamed(
        context, '/home'
    );

  }

   goToSignUp(BuildContext context) {
    Navigator.pushReplacementNamed(
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
                  labelText: "Login",
                  controller: usernameController,
                  hintText: "Username",
                  obscureText: false,
                ),

                const SizedBox(height: 30),

                BeaverTextField(
                  labelText: "Password",
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