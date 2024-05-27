import 'dart:ffi';

class AllLikesMadeDto {
  final int todayLikes;
  final int allDaysLikes;
  AllLikesMadeDto({
    required this.todayLikes,
    required this.allDaysLikes,
  });

  factory AllLikesMadeDto.fromJson(dynamic json) =>
      AllLikesMadeDto(
        todayLikes: json["todayLikes"],
        allDaysLikes: json["allDaysLikes"]
      );
}