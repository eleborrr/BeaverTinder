class User {
  final String firstName;
  final String lastName;
  final String userName;
  final String password;
  final String confirmPassword;
  final String image;
  final String about;
  final String gender;
  final double latitude;
  final double longitude;
  final String subName;
  final DateTime subExpiresDateTime;

  User({required this.firstName,
    required this.lastName,
    required this.userName,
    required this.password,
    required this.confirmPassword,
    required this.image,
    required this.about,
    required this.gender,
    required this.latitude,
    required this.longitude,
    required this.subName,
    required this.subExpiresDateTime,
  });

  factory User.fromJson(dynamic json) =>
      User(
          firstName: json["firstName"],
          lastName: json["lastName"],
          userName: json["userName"],
          password: json["password"],
          confirmPassword: json["confirmPassword"],
          image: json["image"],
          about: json["about"],
          gender: json["gender"],
          latitude: json["latitude"],
          longitude: json["longitude"],
          subName: json["subName"],
          subExpiresDateTime: json["subExpiresDateTime"]);
}