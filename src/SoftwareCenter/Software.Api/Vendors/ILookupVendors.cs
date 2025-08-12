
namespace Software.Api.Vendors;

public interface ILookupVendors
{
    Task<VendorDetailsModel?> GetVendorByIdAsync(Guid id, CancellationToken token);
}