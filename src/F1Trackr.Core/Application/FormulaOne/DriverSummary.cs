using F1Trackr.Core.Domain;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed record DriverSummary(
    DriverId Id,
    string GivenName,
    string FamilyName,
    string? Nationality,
    string? Code,
    string? DriverNumber,
    string? ImageAddress);
