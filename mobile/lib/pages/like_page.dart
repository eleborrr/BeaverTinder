import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/components/shared/alert_window.dart';
import 'package:mobile/dto/likes/like_request_dto.dart';
import 'package:mobile/dto/likes/search_user_result_dto.dart';
import 'package:mobile/main.dart';
import 'package:mobile/navigation/navigation_routes.dart';
import 'package:mobile/services/likes_service.dart';
import '../components/like/beaver_card.dart';
import '../components/shared/beaver_auth_provider.dart';

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
            ? "Can't find new person"
            : response.success!.message == null
              ? "Unexpected error"
              : response.success!.message!,
          () => {
            Navigator.of(context)
            ..pop()
            ..pushNamed(NavigationRoutes.home)
          }
      );

    }

    searchedUser = response.success!;

    setState(() {}); // Обновляем состояние, чтобы перерисовать UI
  }

  List<Map<String, dynamic>> cards = [
    {
  'firstName': 'John',
  'lastName': 'Doe',
  'age': 1,
  'gender': 'Man',
  'about': 'Lorem ipsum dolor sit amet',
  'image': 'lib/images/profile.png',
}, {
      'firstName': 'John',
      'lastName': 'Doe',
      'age': 2,
      'gender': 'Man',
      'about': 'Lorem ipsum dolor sit amet',
      'image': 'lib/images/profile.png',
    },
    {
      'firstName': 'John',
      'lastName': 'Doe',
      'age': 3,
      'gender': 'Man',
      'about': 'Lorem ipsum dolor sit amet',
      'image': 'lib/images/profile.png',
    },
  ];
  int currentIndex = 0;

  void nextCard() {
    setState(()  {
      fetchData();
    });
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
    final authProvider = getit<AuthProvider>();
    final token = authProvider.jwtToken;
    return BeaverScaffold(
      title: "Likes page",
      body: cards.isEmpty
          ? Center(
        child: CircularProgressIndicator(),
      )
          : Column(
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
