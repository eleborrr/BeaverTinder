import 'package:flutter/material.dart';

class BeaverSubscriptionProfileCard extends StatelessWidget {
  final String info;
  final String name;
  final VoidCallback onClick;

  const BeaverSubscriptionProfileCard({
    super.key,
    required this.info,
    required this.name,
    required this.onClick,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16.0),
      decoration: BoxDecoration(
        border: Border.all(color: Colors.grey),
        borderRadius: BorderRadius.circular(8.0),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Subscription info',
            style: TextStyle(fontSize: 18.0, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 8.0),
          Text(
            name,
            style: const TextStyle(fontSize: 16.0, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 8.0),
          Text(
            info,
            style: const TextStyle(fontSize: 14.0),
          ),
          const SizedBox(height: 16.0),
          ElevatedButton(
            onPressed: onClick,
            child: const Text('Extend now'),
          ),
        ],
      ),
    );
  }
}
