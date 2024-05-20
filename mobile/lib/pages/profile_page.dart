import 'package:flutter/material.dart';
import 'package:mobile/components/shared/alert_window.dart';
import 'package:mobile/components/shared/beaver_scaffold.dart';
import 'package:mobile/main.dart';
import 'package:mobile/navigation/navigation_routes.dart';
import 'package:mobile/services/account_service.dart';
import '../components/shared/beaver_auth_provider.dart';
import '../components/shared/beaver_button.dart';
import '../components/shared/beaver_textfield.dart';
import '../instances/user.dart';

class ProfilePage extends StatefulWidget {
  const ProfilePage({super.key});

  @override
  _ProfilePageState createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  late bool changing;
  late User user;
  final authProvider = getit<AuthProvider>();
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
    changing = false;
    fetchUserData();
  }

  Future<void> fetchUserData() async {
    var resp = await _accountService.getUserInfoAsync();
    
    
    setState(() {
      user = resp.success!;
      usernameController.text = user.userName;//userData['userName'];
      firstnameController.text = user.firstName;//userData['firstName'];
      lastnameController.text = user.lastName;//userData['lastName'];
      aboutController.text = user.about;//userData['about'];
      genderController.text = user.gender;//userData['gender'];
      geolocationController.text = "${user.latitude} ${user.longitude}";//"${userData['latitude']}  ${userData['longitude']}";
    });
  }

  changeUser(BuildContext context) async {
      List<String> location = geolocationController.text.split(' ');
      double latitude = 0.0;
      double longitude = 0.0;

      try{
        latitude = double.parse(location[0]);
        longitude = double.parse(location[1]);
      } catch (e){
        return showAlertDialog(context, "Geolocation is invalide, please retry again");
      }

      var newUserData = User(
          firstName: firstnameController.text,
          lastName: lastnameController.text,
          userName: usernameController.text,
          password: user.password,
          confirmPassword: user.confirmPassword,
          image: user.image,
          about: aboutController.text,
          gender: genderController.text,
          latitude: latitude,
          longitude: longitude,
          subName: user.subName,
          subExpiresDateTime: user.subExpiresDateTime
      );

      await _accountService.editUserInfoAsync(newUserData);
  }

  LogoutUser(BuildContext context) {
    authProvider.deleteJwtToken();
    Navigator.of(context)
      ..pop()
      ..pushNamed(NavigationRoutes.login);
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Profile Page",
      body: ListView(
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
              labelText: "Gender",
              controller: genderController,
              hintText: "Gender Man / Woman",
              obscureText: false
          ),

          const SizedBox(height: 10.0),

          BeaverTextField(
              labelText: "About",
              controller: aboutController,
              hintText: "Tell about yourself",
              obscureText: false
          ),

          const SizedBox(height: 10.0),

          BeaverTextField(
              labelText: "Geolocation",
              controller: geolocationController,
              hintText: "Geolocation",
              obscureText: false
          ),

          const SizedBox(height: 10.0),

          BeaverButton(
            buttonText: "Change",
            onTap: () => changeUser(context),
          ),
          const SizedBox(height: 10.0),
          BeaverButton(
            buttonText: "Logout",
            onTap: () => LogoutUser(context),
          ),
          const SizedBox(height: 10.0),
        ],
      ),
    );
  }
}
