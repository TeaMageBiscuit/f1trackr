using F1Trackr.Core.Domain.Management;

namespace F1Trackr.Core.Application.Groups;

public sealed record GroupOverview(
    GroupId Id,
    string Name,
    string Season,
    IEnumerable<GroupMemberSummary> Members);
