import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:mobile/components/shared/beaver_scaffold.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/account_service.dart';
import '../components/shared/beaver_auth_provider.dart';
import '../components/shared/beaver_button.dart';
import '../components/shared/beaver_textfield.dart';

class ProfilePage extends StatefulWidget {
  const ProfilePage({super.key});

  @override
  _ProfilePageState createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  late String token;
  late bool changing;
  final AccountServiceBase _accountService = getit<AccountServiceBase>();

  final lastnameController = TextEditingController();
  final firstnameController = TextEditingController();
  final usernameController = TextEditingController();
  final birthdateController = TextEditingController();
  final passwordController = TextEditingController();
  final cPasswordController = TextEditingController();
  final genderController = TextEditingController();
  final aboutController = TextEditingController();
  final geolocationController = TextEditingController();

  @override
  void initState() {

    super.initState();
    final authProvider = getit<AuthProvider>();
    token = authProvider.jwtToken!;
    changing = false;
    fetchUserData();

  }

  Future<void> fetchUserData() async {
    var resp = await _accountService.getUserInfoAsync("5385e503-b6dc-4f99-8922-8de0b119307f");
    // Мокирование данных пользователя
    const response = '''
    {
      "subName": "Example Subscription",
      "subExpiresDateTime": "2024-04-08T13:44:00.7989673",
      "firstName": "John",
      "lastName": "Doe",
      "userName": "johndoe",
      "longitude": 37.64,
      "latitude": 55.76,
      "image": "https://example.com/profile.jpg",
      "about": "Lorem ipsum dolor sit amet",
      "gender": "Male"
    }
  ''';
  print(resp.success);
    final Map<String, dynamic> userData = jsonDecode(response);
    setState(() {
      usernameController.text = resp.success!.userName;//userData['userName'];
      firstnameController.text = resp.success!.firstName;//userData['firstName'];
      lastnameController.text = resp.success!.lastName;//userData['lastName'];
      aboutController.text = resp.success!.about;//userData['about'];
      genderController.text = resp.success!.gender;//userData['gender'];
      geolocationController.text = "${resp.success!.latitude} ${resp.success!.longitude}";//"${userData['latitude']}  ${userData['longitude']}";
    });
  }

  void handleMapClick(Map<String, double> coords) {
    // setState(() {
    //   latitude = coords['latitude'];
    //   longitude = coords['longitude'];
    //   location = '${coords['latitude']}, ${coords['longitude']}';
    // });
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Profile Page",
      body: ListView(
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
            buttonText: "Change",
            onTap: () => changeUser(context),
          ),
        ],
      ),
    );
  }
}

changeUser(BuildContext context) {

}
