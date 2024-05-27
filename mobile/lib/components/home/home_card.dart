import 'package:flutter/material.dart';
import 'package:mobile/styles/home_style.dart';

class HomeCardArgs {
  final String imageAddress;
  final String headerText;
  final String descriptionText;

  const HomeCardArgs( {
    required this.imageAddress,
    required this.headerText,
    required this.descriptionText});
}

class HomeCard extends StatelessWidget {
  const HomeCard({
  super.key,
    required this.args
  });
  final HomeCardArgs args;


  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: Container(
            alignment: Alignment.center,
            child: Image.asset(
              args.imageAddress,
              height: 100.0,
              fit: BoxFit.cover,
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              Text(
                args.headerText,
                style: const HomeStyles().getCardStyle(),//TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                args.descriptionText,
                textAlign: TextAlign.center,
              ),
            ],
          ),
        ),
      ],
    );
  }
}
