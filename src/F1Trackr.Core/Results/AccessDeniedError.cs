using FluentResults;

namespace F1Trackr.Core.Results;

public sealed class AccessDeniedError : Error
{
    public AccessDeniedError(string message)
        : base(message)
    {
    }
}
