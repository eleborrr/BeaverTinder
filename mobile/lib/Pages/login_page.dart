import 'package:flutter/material.dart';
import 'package:mobile/Components/beaver_textfield.dart';

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("lib/images/pageheader.png"),
            fit: BoxFit.cover,
          )
        ),
        child: SafeArea(
          child: Center(
            child: Column(
              children: [
                Image.asset("lib/images/logo.png"),

                BeaverTextField(),

                const SizedBox(height: 30,),

                BeaverTextField(),
              ],
            )
          ),
        ),
      ),

    );
  }

}