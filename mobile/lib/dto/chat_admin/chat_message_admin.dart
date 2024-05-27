class ChatMessageAdmin {
  final String senderName;
  final String content;
  final DateTime timestamp;

  ChatMessageAdmin({
    required this.senderName,
    required this.content,
    required this.timestamp,
  });

  factory ChatMessageAdmin.fromJson(Map<String, dynamic> json) {
    return ChatMessageAdmin(
      senderName: json['senderName'] as String,
      content: json['content'] as String,
      timestamp: json['timestamp'] as DateTime,
    );
  }
}