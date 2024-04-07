import 'package:flutter/material.dart';
import 'package:mobile/Components/home/home_card.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/styles/home_style.dart';

import '../Components/beaver_drawer.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  List<HomeCardArgs> cardArgs = <HomeCardArgs>[
    const HomeCardArgs(
      headerText: "Simple To Use",
      descriptionText: "Nothing useless.",
      imageAddress: "lib/images/home/01.png"
    ),
    const HomeCardArgs(
        headerText: "Without VPN",
        descriptionText: "Use our site and don't mind about VPN ;).",
        imageAddress: "lib/images/home/02.png"
    ),
    const HomeCardArgs(
        headerText: "Very Fast",
        descriptionText: "Don’t waste your time! Begin communicate right now!",
        imageAddress: "lib/images/home/03.png"
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Home Page",
      body: Container(
        decoration: const BoxDecoration(
            image: DecorationImage(
              image: AssetImage("lib/images/pageheader.png"),
              fit: BoxFit.cover,
            )
        ),

        child:
          Container(
            padding: const EdgeInsets.all(100),
            alignment: Alignment.center,

            child:  Column(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Beaver Tinder Super Powers',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 10),
                const Text(
                  'Our dating platform is like a breath of fresh air. Clean and trendy design with ready to use features we are sure you will love.',
                  textAlign: TextAlign.center,
                ),
                Expanded(
                  child: ListView.builder(
                    shrinkWrap: true,
                    itemCount: cardArgs.length,
                    itemBuilder: (context, index) {
                      return HomeCard(args: cardArgs[index]);
                    },
                  ),
                ),
              ],
            ),
          ),
      ),
    );
  }
}


