class ChatMessageDto {
  final String senderUserName;
  final String message;
  final String filenames;

  ChatMessageDto({
    required this.senderUserName,
    required this.message,
    required this.filenames,
  });

  factory ChatMessageDto.fromJson(Map<String, dynamic> json) {
    return ChatMessageDto(
      senderUserName: json['senderUserName'] as String,
      message: json['message'] as String,
      filenames: json['roomName'] as String,
    );
  }
}