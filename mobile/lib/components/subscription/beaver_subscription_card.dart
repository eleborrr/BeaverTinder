import 'package:flutter/material.dart';

class BeaverSubscriptionCard extends StatelessWidget {
  final String name;
  final String price;
  final String info;
  final VoidCallback onClick;

  const BeaverSubscriptionCard({super.key, required this.name, required this.price, required this.info, required this.onClick});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(color: Colors.black),
        borderRadius: BorderRadius.circular(10.0),
      ),
      padding: EdgeInsets.all(16.0),
      margin: EdgeInsets.all(16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            name,
            style: const TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 20.0,
            ),
          ),
          Text(
            '$price ₽ / Per Month',
            style: const TextStyle(
              fontSize: 18.0,
            ),
          ),
          const SizedBox(height: 8.0),
          const Text(
            'Subscription info',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 16.0,
            ),
          ),
          const SizedBox(height: 4.0),
          Text(
            info,
            style: TextStyle(
              fontSize: 14.0,
            ),
          ),
          const SizedBox(height: 16.0),
          ElevatedButton(
            onPressed: onClick,
            child: Text('Purchase now'),
          ),
        ],
      ),
    );
  }
}
