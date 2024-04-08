
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class BeaverPaymentForm extends StatefulWidget {
  final VoidCallback onClose;
  final int userId;
  final int subsId;
  final double amount;

  BeaverPaymentForm({required this.onClose, required this.userId, required this.subsId, required this.amount});

  @override
  _BeaverPaymentFormState createState() => _BeaverPaymentFormState();
}

class _BeaverPaymentFormState extends State<BeaverPaymentForm> {
  late String cardNumber;
  late int month;
  late int year;
  late String code;
  String? err;
  bool downloading = false;

  void handleClick() async {
  }

  bool validate() {
    if (!RegExp(r'[0-9]{13,16}').hasMatch(cardNumber)) {
      err =
      'неверный номер карты: пишите без пробелов, номер карты от 13 до 16 символов';
      return false;
    }
    if (!RegExp(r'[0-9]{3}').hasMatch(code)) {
      err = 'неверный код карты: код карты с обратной стороны, 3 символа';
      return false;
    }
    if (month < 1 || month > 12) {
      err = 'неверно указан месяц: от 1 до 12';
      return false;
    }
    final endDate = DateTime(year, month);
    final now = DateTime.now();
    if (endDate.isBefore(now)) {
      err = 'срок действия карты истёк';
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
            'Детали оплаты',
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
              labelText: 'Номер карты',
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
                    labelText: 'Месяц',
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
                    labelText: 'Год',
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
            child: Text('Оплатить'),
          ),
        ],
      ),
    );
  }

}