using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetChatById;

public record GetSupportChatByIdQuery(string CurUserId, string UserId) : IQuery<SupportRoom>;