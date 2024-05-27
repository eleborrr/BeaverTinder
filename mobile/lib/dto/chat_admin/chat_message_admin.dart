class ChatMessageAdmin {
  final String senderName;
  final String content;

  ChatMessageAdmin({
    required this.senderName,
    required this.content,
  });

  factory ChatMessageAdmin.fromJson(Map<String, dynamic> json) {
    return ChatMessageAdmin(
      senderName: json['senderName'] as String,
      content: json['content'] as String,
    );
  }
}