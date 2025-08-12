
using Marten;

namespace Software.Api.Vendors;

public class MartenVendorData(IDocumentSession session) : ICreateVendors, ILookupVendors
{
    public async Task<VendorDetailsModel> CreateVendorAsync(VendorCreateModel request)
    {
        // create the thing to save in the database, save it(?) return a VendorDetailsModel
        // create insert statement, run it the database.
        /// Mapping - Getting from Point A -> Point B

        var vendorToSave = request.MapToEntity(Guid.NewGuid(), "slime");

        session.Store(vendorToSave);
        await session.SaveChangesAsync();
        var response = new VendorDetailsModel(vendorToSave.Id, vendorToSave.Name, vendorToSave.Url, vendorToSave.Contact, vendorToSave.CreatedBy, vendorToSave.CreatedOn);
        return response;
    }

    public async Task<VendorDetailsModel?> GetVendorByIdAsync(Guid id, CancellationToken token)
    {
        var entity = await session.Query<VendorEntity>().Where(v => v.Id == id).SingleOrDefaultAsync(token);
        if (entity == null)
        {
            return null;
        } else
        {
            return entity.MapToDetails(); 
        }
    }
}


public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public PointOfContact? Contact { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedOn { get; set; }

    public VendorDetailsModel MapToDetails()
    {
        return new VendorDetailsModel(Id, Name, Url, Contact!, CreatedBy, CreatedOn);
    }
}