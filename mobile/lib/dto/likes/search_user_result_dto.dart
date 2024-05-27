class SearchUserResultDto {
  final String id;
  final String firstName;
  final String lastName;
  final int age;
  final String gender;
  final String about;
  final bool successful;
  final String? message;
  final String? distanceInKm;
  final int statusCode;
  final String image;

  SearchUserResultDto({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.age,
    required this.gender,
    required this.about,
    required this.successful,
    required this.message,
    required this.distanceInKm,
    required this.statusCode,
    required this.image
  });

  factory SearchUserResultDto.fromJson(dynamic json) =>
      SearchUserResultDto(
          id: json["id"],
          firstName: json["firstName"],
          lastName: json["lastName"],
          age: json["age"],
          gender: json["gender"],
          about: json["about"],
          successful: json["successful"],
          message: json["message"],
          distanceInKm: json["distanceInKm"],
          statusCode: json["statusCode"],
          image: json["image"]
      );
}