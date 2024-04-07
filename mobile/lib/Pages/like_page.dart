import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import '../Components/like/beaver_card.dart';
import '../Components/shared/beaver_drawer.dart';

class LikePage extends StatefulWidget {
  const LikePage({super.key});

  @override
  _LikePageState createState() => _LikePageState();
}

class _LikePageState extends State<LikePage> {
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
    setState(() {
      currentIndex = (currentIndex + 1) % cards.length;
    });
  }

  void previousCard() {
    setState(() {
      currentIndex = (currentIndex - 1 + cards.length) % cards.length;
    });
  }

  void like() {
    print('Liked');
    nextCard();
  }

  void dislike() {
    print('Disliked');
    nextCard(); // Переход к следующей карточке
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Like Page",
      body: cards.isEmpty
          ? Center(
        child: CircularProgressIndicator(),
      )
          : Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          BeaverCard(profile: cards[currentIndex], like: like, dislike: dislike),
        ],
      ),
    );
  }
}
