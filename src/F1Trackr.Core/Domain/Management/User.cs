namespace F1Trackr.Core.Domain.Management;

public sealed class User
{
    public required UserId Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string AccessCode { get; set; }

    public bool IsAdmin { get; set; }
}
