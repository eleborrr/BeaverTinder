import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/components/shared/alert_window.dart';
import 'package:mobile/dto/likes/like_request_dto.dart';
import 'package:mobile/dto/likes/search_user_result_dto.dart';
import 'package:mobile/main.dart';
import 'package:mobile/navigation/navigation_routes.dart';
import 'package:mobile/services/likes_service.dart';
import '../components/like/beaver_card.dart';

class LikePage extends StatefulWidget {

  LikePage({super.key});

  @override
  _LikePageState createState() => _LikePageState();
}

class _LikePageState extends State<LikePage> {
  final LikesServiceBase likesService = getit<LikesServiceBase>();

  SearchUserResultDto? searchedUser = null;

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  Future<void> fetchData() async {
    final response = await likesService.getNewUserAsync();
    if(response.success == null)
    {
      showAlertDialog(context,
          response.success == null
            ? "Wait new users!"
            : response.success!.message == null
              ? "Unexpected error"
              : response.success!.message!,
          () {
            Navigator.of(context)
            ..pop()
            ..pushNamed(NavigationRoutes.home);
          }
      );
      return;
    }

    searchedUser = response.success!;

    setState(() {}); // Обновляем состояние, чтобы перерисовать UI
  }

  void nextCard() {
      fetchData();
  }

  void like() async {
    await likesService.likeUserAsync(
        LikeRequestDto(likedUserId: searchedUser!.id)
    );
    nextCard();
  }

  void dislike() async {
    await likesService.dislikeAsync(
        LikeRequestDto(likedUserId: searchedUser!.id)
    );
    nextCard(); // Переход к следующей карточке
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Likes page",
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: searchedUser == null
        ? []
        : [
            BeaverCard(
                profile: searchedUser!,
                like: like,
                dislike: dislike
            ),
        ],
      ),
    );
  }
}
