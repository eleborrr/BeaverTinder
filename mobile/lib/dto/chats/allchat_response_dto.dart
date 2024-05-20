class AllChatResponseDto {
  final String firstName;
  final String lastName;
  final String userName;
  final String image;

  AllChatResponseDto({
    required this.firstName,
    required this.lastName,
    required this.userName,
    required this.image,
  });

  factory AllChatResponseDto.fromJson(Map<String, dynamic> json) {
    return AllChatResponseDto(
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
      userName: json['userName'] as String,
      image: json['image'] as String,
    );
  }
}