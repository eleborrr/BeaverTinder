import 'package:mobile/dto/response_base_dto.dart';

class SubscriptionInfoDto {
  final int id;
  final String name;
  final double pricePerMonth;
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