class LoginRequestDto {

  final String  userName;
  final String  password;
  final bool  rememberMe;

  LoginRequestDto(
      this.userName,
      this.password,
      this.rememberMe
      );
}