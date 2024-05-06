class RegisterRequestDto {

  final String  lastName;
  final String  firstName;
  final String  userName;
  final String  email;
  final String  password;
  final String  confirmPassword;
  final String  dateOfBirth;
  final double  latitude;
  final double  longitude;
  final String  gender;
  final String  about;

  RegisterRequestDto(
      this.lastName,
      this.firstName,
      this.userName,
      this.email,
      this.password,
      this.confirmPassword,
      this.dateOfBirth,
      this.latitude,
      this.longitude,
      this.gender,
      this.about);
}