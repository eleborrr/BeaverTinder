import 'package:flutter/material.dart';
import 'package:mobile/Components/shared/beaver_scaffold.dart';
import 'package:mobile/dto/subscription/subscription_info_dto.dart';
import 'package:mobile/services/subscription_service.dart';
import 'package:mobile/main.dart';
import '../Components/subscription/beaver_payment_form.dart';
import '../Components/subscription/beaver_subscription_card.dart';

class SubscriptionPage extends StatefulWidget {
  @override
  _SubscriptionPageState createState() => _SubscriptionPageState();
}

class _SubscriptionPageState extends State<SubscriptionPage> {
  final SubscriptionServiceBase _subService = getit<SubscriptionServiceBase>();
  bool _disable = true;
  List<SubscriptionInfoDto>? _paymentArr;
  int? _subsId;
  double? _amount;

  void handleClick(int subsId, double amount) {
    setState(() {
      _disable = false;
      _subsId = subsId;
      _amount = amount;
    });
  }

  void onClose() {
    setState(() {
      _disable = true;
    });
  }

  @override
  void initState() {
    super.initState();
    fetchData();
  }


  Future<void> fetchData() async {
    final response = await _subService.getAllSubscriptionsAsync();
    if(response == null)
    {
      return;
    }

    _paymentArr = response.success;
// Мок-данные для подписок
//     _paymentArr = [
//       {
//         'id': 1,
//         'name': 'Basic Subscription',
//         'description': 'Basic features for beginners',
//         'pricePerMonth': '9.99',
//       },
//       {
//         'id': 2,
//         'name': 'Premium Subscription',
//         'description': 'Advanced features for professionals',
//         'pricePerMonth': '19.99',
//       },
//       {
//         'id': 3,
//         'name': 'Pro Subscription',
//         'description': 'Premium features for experts',
//         'pricePerMonth': '29.99',
//       },
//     ];

    setState(() {}); // Обновляем состояние, чтобы перерисовать UI
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _disable
          ? _buildSubscriptionCards()
          : BeaverPaymentForm(
          onClose: onClose,
          userId: 1,
      subsId: _subsId!,
      amount: _amount!,
    ),
    );
  }

  Widget _buildSubscriptionCards() {
    return BeaverScaffold(
      title: "Shop page",
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: _paymentArr == null
                ? [Container()]
                : _paymentArr!.map((p) {
              return Padding(
                padding: const EdgeInsets.only(bottom: 16.0),
                child: BeaverSubscriptionCard(
                  info: p.description,
                  name: p.name,
                  price: p.pricePerMonth.toString(),
                  onClick: () =>
                      handleClick(p.id, p.pricePerMonth),
                ),
              );
            }).toList(),
          ),
        ),
      ),
    );
  }
}
