class SubscriptionInfoDto {
  final int id;
  final String name;
  final int pricePerMonth;
  final String description;


  SubscriptionInfoDto(
      this.id,
      this.name,
      this.pricePerMonth,
      this.description);


  factory SubscriptionInfoDto.fromJson(dynamic json)
  {
      return SubscriptionInfoDto(
        json["id"],
        json["name"],
        json["pricePerMonth"],
        json["description"]
      );
  }
}