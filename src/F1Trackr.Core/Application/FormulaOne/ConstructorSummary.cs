using F1Trackr.Core.Domain;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed record ConstructorSummary(
    ConstructorId Id,
    string Name,
    string? Nationality,
    string? LogoAddres);
