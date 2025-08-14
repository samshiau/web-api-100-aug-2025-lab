namespace Software.Api.CatalogItems;

public interface ICheckForVendors
{
    Task<bool> DoesVendorExistAsync(Guid id, CancellationToken token);
}