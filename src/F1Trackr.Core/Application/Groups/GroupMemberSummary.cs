using F1Trackr.Core.Domain.Management;

namespace F1Trackr.Core.Application.Groups;

public sealed record GroupMemberSummary(
    GroupId GroupId,
    UserId UserId,
    string UserName,
    int CurrentScore);
