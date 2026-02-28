using FluentValidation;

namespace F1Trackr.Server.Setup;

public static class ValidatorSetup
{
    public static void SetupValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(
            BackendAssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}
