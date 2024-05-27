import 'package:dart_amqp/dart_amqp.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/home/home_card.dart';
import 'package:mobile/components/shared/alert_window.dart';
import 'package:mobile/components/shared/beaver_scaffold.dart';
import 'package:mobile/main.dart';
import 'package:mobile/navigation/navigation_routes.dart';
import 'package:mobile/services/dio_client.dart';
import 'package:mobile/services/likes_made_service.dart';

class FunFactPage extends StatefulWidget {
  FunFactPage({super.key});
  final Client client = Client(settings: ConnectionSettings(
      host: baseIp,
      port: 5672,
      authProvider: PlainAuthenticator('rabbitsa', 'mypa55w0rd!')
  ));
  @override
  State<FunFactPage> createState() => _FunFactPageState();
}

class _FunFactPageState extends State<FunFactPage> {
  final LikesMadeServiceBase likesMadeService = getit<LikesMadeServiceBase>();
  final DioClient dioClient = getit<DioClient>();
  int allDaysLikes = 0;
  int todayLikes = 0;
  List<HomeCardArgs> cardArgs = [];

  void setCardArgs() {
    cardArgs = <HomeCardArgs>[
      HomeCardArgs(
          headerText: allDaysLikes.toString(),
          descriptionText: "Likes for all days",
          imageAddress: "lib/images/home/01.png"
      ),
      HomeCardArgs(
          headerText: todayLikes.toString(),
          descriptionText: "Likes made today",
          imageAddress: "lib/images/home/02.png"
      )
    ];
  }
  @override
  void initState() {
    super.initState();
    fetchData();
    setCardArgs();
    setupListener();
  }
  void fetchData() async {
    var response = await likesMadeService.getAllLikesMadeAsync();
    response.match(
            (value) => {
              setState(() {
                allDaysLikes = value.allDaysLikes;
                todayLikes = value.todayLikes;
                setCardArgs();
              })
            },
            (error) => {
              showAlertDialog(context, error,
                () {
                Navigator.of(context)
                ..pop()
                ..pushNamed(NavigationRoutes.home);
                }
              )
            });

  }

  Future<void> setupListener() async {
    var queueName = await dioClient.connectUserToQueue();
    if (queueName == null)
      return;
    final channel = await widget.client.channel();
    final exchange = await channel.exchange(
        'likes-exchange',
        ExchangeType.DIRECT
    );
    final consumer = await exchange.bindQueueConsumer(
        queueName,
        ["routingKey"],
        exclusive: false,
        autoDelete: true,
        declare: false
    );
    consumer.listen((_) => setState(() {
      allDaysLikes++;
      todayLikes++;
      setCardArgs();
    }));
  }

  @override
  Widget build(BuildContext context) {
    return BeaverScaffold(
      title: "Fun Fact",
      body: PopScope(
        canPop: true,
        onPopInvoked: (didPop) {
        widget.client.close();
        if (didPop) {
          return;
        }
        Navigator.pop(context);
      },
      child:
        Container(
          padding: const EdgeInsets.all(100),
          alignment: Alignment.center,

          child:  Column(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              const Text(
                'Fun facts:',
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 10),
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


