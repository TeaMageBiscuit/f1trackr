using F1Trackr.Core.Domain.Management;

namespace F1Trackr.Core.Application.Groups;

public sealed record GroupSummary(
    GroupId Id,
    string Name,
    string Season);
