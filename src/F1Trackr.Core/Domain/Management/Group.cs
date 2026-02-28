namespace F1Trackr.Core.Domain.Management;

public sealed class Group
{
    public required GroupId Id { get; set; }

    public required string Name { get; set; }

    public required string Season { get; set; }

    public ICollection<GroupMember> Members { get; set; } = [];
}
