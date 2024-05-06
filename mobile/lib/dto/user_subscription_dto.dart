class UserSubscriptionDto {
  final int id;
  final String name;
  final DateTime expires;

  UserSubscriptionDto({
    required this.id,
    required this.name,
    required this.expires,
  });

  factory UserSubscriptionDto.fromJson(dynamic json) =>
      UserSubscriptionDto(
          id: json["id"],
          name: json["name"],
          expires: json["expires"]);
}