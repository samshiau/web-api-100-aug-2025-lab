using FluentValidation;

namespace Software.Api.Vendors;

public static class Extensions
{
    public static ServiceCollection AddVendors(this ServiceCollection services)
    {
        services.AddScoped<ICreateVendors, MartenVendorData>();
        services.AddScoped<ILookupVendors, MartenVendorData>();
        services.AddScoped<IValidator<VendorCreateModel>, VendorCreateModelValidator>();
        return services;
    }
}
