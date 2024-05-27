class GeolocationResponseDto {
  final String id;
  final String userId;
  final double latitude;
  final double longitude;

  GeolocationResponseDto({
    required this.id,
    required this.userId,
    required this.latitude,
    required this.longitude
  });

  factory GeolocationResponseDto.fromJson(dynamic json) =>
      GeolocationResponseDto(
        id: json["id"],
        userId: json["userId"],
        latitude: json["latitude"],
        longitude: json["longitude"],
      );
}