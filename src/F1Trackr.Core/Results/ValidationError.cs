using FluentResults;

namespace F1Trackr.Core.Results;

public sealed class ValidationError : Error
{
    public ValidationError(string message)
        : this(string.Empty, message)
    {
    }

    public ValidationError(string propertyName, string message)
        : base(message)
    {
        PropertyName = propertyName;
    }

    public ValidationError(string propertyName, string message, params object[] args)
        : this(propertyName, string.Format(message, args))
    {
    }

    public string PropertyName { get; }
}
