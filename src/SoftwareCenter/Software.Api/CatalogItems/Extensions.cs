using Marten;
using Microsoft.AspNetCore.Mvc;
using Software.Api.CatalogItems.Endpoints;
using Software.Api.Vendors;

namespace Software.Api.CatalogItems;




public static class Extensions
{

    public static IServiceCollection AddCatalogItems(this IServiceCollection services)
    {
        services.AddScoped<ILookupVendors, MartenVendorData>();
        return services;
    }
    public static WebApplication MapCatalogItems(this WebApplication builder)
    {
        builder.MapGet("/catalog-items", async ([FromServices] IDocumentSession session) =>
        {
            return await session.Query<CatalogItemEntity>().ToListAsync();
        }).RequireAuthorization();
        var group = builder.MapGroup("/vendors").RequireAuthorization(); // unless you are identified with a JWT

        //group.MapGet("/{id:guid}/catalog-items", (CatalogItemCreateRequest request) => request);
        // GET /vendors/{id}
        // group.MapGet("/{id:guid}/catalog-items/{itemId:guid}", (CatalogItemCreateRequest request) => request);


        group.MapPost("/{id:guid}/catalog-items", AddCatalogItemEndpoint.AddCatalogItemAsync).RequireAuthorization("SoftwareCenter"); // and you are SoftwareCenter
       
        return builder;
    }
}


public record CatalogItemCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}

public record CatalogItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public Guid VendorId { get; set; }
}

public class CatalogItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public Guid VendorId { get; set; }
}
