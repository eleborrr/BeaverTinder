class LikeRequestDto {
  final String likedUserId;

  LikeRequestDto({
    required this.likedUserId
  });

  factory LikeRequestDto.fromJson(dynamic json) =>
      LikeRequestDto(
          likedUserId: json["likedUserId"]
      );
}