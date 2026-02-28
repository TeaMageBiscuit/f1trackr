using FluentResults;

namespace F1Trackr.Core.Results;

public sealed class NotFoundError : Error
{
    public NotFoundError(string message)
        : base(message)
    {
    }
}
