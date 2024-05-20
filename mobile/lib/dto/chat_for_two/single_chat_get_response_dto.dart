class SingleChatGetResponse {
  final String senderName;
  final String receiverName;
  final String roomName;

  SingleChatGetResponse({
    required this.senderName,
    required this.receiverName,
    required this.roomName,
  });

  factory SingleChatGetResponse.fromJson(Map<String, dynamic> json) {
    return SingleChatGetResponse(
      senderName: json['senderName'] as String,
      receiverName: json['receiverName'] as String,
      roomName: json['roomName'] as String,
    );
  }
}