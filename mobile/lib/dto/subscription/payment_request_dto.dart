class PaymentRequestDto {
  late String userId;
  final String cardNumber;
  final int month;
  final double amount;
  final int year;
  final String code;
  final int subsId;


  PaymentRequestDto({
    required this.userId,
    required this.cardNumber,
    required this.month,
    required this.amount,
    required this.year,
    required this.code,
    required this.subsId
  });


  factory PaymentRequestDto.fromJson(dynamic json)
  {
    return PaymentRequestDto(
        userId: "userId",
        cardNumber: json["cardNumber"],
        month: json["month"],
        amount: json["amount"],
        year: json["year"],
        code: json["code"],
        subsId: json["subsId"]
    );
  }
}

