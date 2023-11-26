using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.SupportChat.GetSupportChatById;

public record GetSupportChatByIdQuery(string CurUserId, string UserId) : IQuery<SupportRoom>;