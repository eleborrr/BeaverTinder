import 'package:flutter/material.dart';

class BeaverCard extends StatelessWidget {
  final Map<String, dynamic> profile;
  final Function like;
  final Function dislike;

  const BeaverCard({super.key, required this.profile, required this.like, required this.dislike});

  String correctWord(int age) {
    if (age % 10 == 1) {
      return 'год';
    } else if (age % 10 == 2 || age % 10 == 3 || age % 10 == 4) {
      return 'года';
    } else {
      return 'лет';
    }
  }

  @override
  Widget build(BuildContext context) {
    bool alreadyLiked = false;

    return Container(
      child: Column(
        children: [
          Container(
            margin: EdgeInsets.symmetric(vertical: 20.0),
            child: Column(
              children: [
                Image.asset(
                  profile['image'],
                  width: 100,
                  height: 100,
                  fit: BoxFit.cover,
                ),
                Text(
                  '${profile['firstName']} ${profile['lastName']}, ${profile['age']} ${correctWord(profile['age'])}',
                ),
                Text(
                  'Пол: ${profile['gender'] == 'Woman' ? 'жен' : 'муж'}',
                ),
                Text(
                  'О себе: ${profile['about']}',
                ),
              ],
            ),
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              IconButton(
                icon: const Icon(Icons.thumb_down),
                onPressed: () {
                  alreadyLiked = true;
                  dislike();
                  alreadyLiked = false;
                },
                disabledColor: alreadyLiked ? Colors.grey : null,
              ),
              IconButton(
                icon: const Icon(Icons.thumb_up),
                onPressed: () {
                  alreadyLiked = true;
                  like();
                  alreadyLiked = false;
                },
                disabledColor: alreadyLiked ? Colors.grey : null,
              ),
            ],
          ),
        ],
      ),
    );
  }
}
