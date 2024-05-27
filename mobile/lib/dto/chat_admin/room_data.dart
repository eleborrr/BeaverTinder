class RoomData {
  final String receiverName;
  final String roomName;
  final String senderName;

  RoomData({
    required this.receiverName,
    required this.roomName,
    required this.senderName,
  });

  factory RoomData.fromJson(Map<String, dynamic> json) {
    return RoomData(
      receiverName: json['receiverName'] as String,
      roomName: json['roomName'] as String,
      senderName: json['senderName'] as String,
    );
  }
}