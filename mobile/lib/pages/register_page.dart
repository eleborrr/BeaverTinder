import 'package:datetime_picker_formfield/datetime_picker_formfield.dart';
import 'package:flutter/material.dart';
import 'package:mobile/dto/register/register_request_dto.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/auth_service.dart';
import 'package:intl/intl.dart';

import '../components/shared/beaver_button.dart';
import '../components/shared/beaver_textfield.dart';

class RegisterPage extends StatefulWidget {
  RegisterPage({super.key});

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final AuthServiceBase _authService = getit<AuthServiceBase>();

  final format = DateFormat("dd-MM-yyyy");

  final lastnameController = TextEditingController();

  final firstnameController = TextEditingController();

  final usernameController = TextEditingController();

  final emailController = TextEditingController();

  DateTime? birthdate;

  final passwordController = TextEditingController();

  final cPasswordController = TextEditingController();

  final genderController = TextEditingController();

  final aboutController = TextEditingController();

  final geolocationController = TextEditingController();

  var selectedGender = "Male";

  void registerUser(BuildContext context) async {


    if(birthdate == null)
      return;
    print(DateFormat("dd.MM.yyyy").format(birthdate!));
    final String lastname = lastnameController.text;
    final String firstname = firstnameController.text;
    final String username = usernameController.text;
    final String email = emailController.text;
    final String password = passwordController.text;
    final String cPassword = cPasswordController.text;
    final String about = aboutController.text;
    final double latitude = 0;
    final double longitude = 0;

    final registerDto = RegisterRequestDto(
        lastname,
        firstname,
        username,
        email,
        password,
        cPassword,
        DateFormat("dd.MM.yyyy").format(birthdate!),
        latitude,
        longitude,
        selectedGender,
        about);

    final registerResponse = await _authService.registerAsync(registerDto);

    if(registerResponse.success == null)
      {
        return;
      }

    if(registerResponse.success!.successful)
      {
        goToSignIn(context);
      }
  }

  goToSignIn(BuildContext context) {
    Navigator.pushReplacementNamed(
        context, '/login'
    );
  }

  changeDate(DateTime? date) {
    if (date == null) {
      return;
    }
    birthdate = date;
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
                    labelText: "Last name",
                    controller: lastnameController,
                    hintText: "Last name",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    labelText: "First name",
                    controller: firstnameController,
                    hintText: "First name",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    labelText: "Username",
                    controller: usernameController,
                    hintText: "Username",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                    labelText: "Email",
                    controller: emailController,
                    hintText: "someEmail@gmail.com",
                    obscureText: false,
                  ),

                  const SizedBox(height: 10.0),

                  Column(children: <Widget>[
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 25.0),
                      child: DateTimeField(
                        format: format,
                        decoration: InputDecoration(
                          hintText: 'Select your birthdate',
                          enabledBorder: const OutlineInputBorder(
                            borderSide: BorderSide(color: Colors.grey),
                          ),
                          focusedBorder: OutlineInputBorder(
                            borderSide: BorderSide(color: Colors.grey.shade400),
                          ),
                          fillColor: Colors.grey.shade200,
                          filled: true,
                        ),
                        onShowPicker: (context, currentValue) async {
                          final selectedDate = await showDatePicker(
                            context: context,
                            firstDate: DateTime(1900),
                            initialDate: currentValue ?? DateTime(2000),
                            lastDate: DateTime.now(),
                          );

                          changeDate(selectedDate);

                          return selectedDate;
                        },
                        onSaved: (date) => changeDate(date),
                      ),
                    ),
                  ]),
              const SizedBox(height: 10.0),

                  BeaverTextField(
                      labelText: "Password",
                      controller: passwordController,
                      hintText: "Password",
                      obscureText: true
                  ),

                  const SizedBox(height: 10.0),

                  BeaverTextField(
                      labelText: "Confirm password",
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
                      labelText: "About",
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
