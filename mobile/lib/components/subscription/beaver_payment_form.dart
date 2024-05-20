import 'package:flutter/material.dart';
import 'package:mobile/components/shared/alert_window.dart';
import 'package:mobile/dto/subscription/payment_request_dto.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/subscription_service.dart';

class BeaverPaymentForm extends StatefulWidget {
  final VoidCallback onClose;
  final int userId;
  final int subsId;
  final int amount;

  BeaverPaymentForm({
    required this.onClose,
    required this.userId,
    required this.subsId,
    required this.amount
  });

  @override
  _BeaverPaymentFormState createState() => _BeaverPaymentFormState();
}

class _BeaverPaymentFormState extends State<BeaverPaymentForm> {
  late String cardNumber;
  late int month;
  late int year;
  late String code;
  final subscriptionService = getit<SubscriptionServiceBase>();
  String? err;
  bool downloading = false;

  void handleClick() async {
      if (!validate())
      {
        showAlertDialog(context, err!);
        return;
      }

      var request = PaymentRequestDto(
          userId: "",
          cardNumber: cardNumber,
          month: month,
          amount: double.parse(widget.amount.toString()),
          year: year,
          code: code,
          subsId: widget.subsId
      );
      var res = await subscriptionService.paySubscriptionAsync(request);
      if (res.success!.isFailure)
        showAlertDialog(context, "Maybe something went wrong, login again and check your opportunities");
      widget.onClose();
  }

  bool validate() {
    if (!RegExp(r'^\d{13,16}$').hasMatch(cardNumber)) {
      err =
      'Invalid card number: write without spaces, card number is between 13 and 16 signs';
      return false;
    }
    if (!RegExp(r'^\d{3,4}$').hasMatch(code)) {
      err = 'Wrong CVV code: 3 or 4 signs';
      return false;
    }
    if (month < 1 || month > 12) {
      err = 'Invalid month: from 1 to 12';
      return false;
    }
    final endDate = DateTime(year, month);
    final now = DateTime.now();
    if (endDate.isBefore(now)) {
      err = 'Card not actual';
      return false;
    }
    return true;
  }

  @override
  Widget build(BuildContext context) {
    return downloading
        ? Center(
      child: CircularProgressIndicator(),
    )
        : Container(
      padding: EdgeInsets.all(16.0),
      child: Column(
        children: [
          Text(
            'Payment details',
            style: TextStyle(
              fontSize: 18.0,
              fontWeight: FontWeight.bold,
            ),
          ),
          IconButton(
            icon: Icon(Icons.close),
            onPressed: widget.onClose,
          ),
          TextFormField(
            onChanged: (value) {
              cardNumber = value;
            },
            decoration: InputDecoration(
              labelText: 'Card number',
            ),
          ),
          Row(
            children: [
              Expanded(
                child: TextFormField(
                  onChanged: (value) {
                    month = int.parse(value);
                  },
                  keyboardType: TextInputType.number,
                  decoration: InputDecoration(
                    labelText: 'Month',
                  ),
                ),
              ),
              SizedBox(width: 10.0),
              Expanded(
                child: TextFormField(
                  onChanged: (value) {
                    year = int.parse(value);
                  },
                  keyboardType: TextInputType.number,
                  decoration: InputDecoration(
                    labelText: 'Year',
                  ),
                ),
              ),
            ],
          ),
          TextFormField(
            onChanged: (value) {
              code = value;
            },
            decoration: InputDecoration(
              labelText: 'CVV/CVC',
            ),
          ),
          ElevatedButton(
            onPressed: handleClick,
            child: Text('Pay'),
          ),
        ],
      ),
    );
  }

}