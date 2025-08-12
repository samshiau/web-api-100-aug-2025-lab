
using Marten;

namespace Software.Api.Vendors;

public class MartenVendorData(IDocumentSession session) : ICreateVendors, ILookupVendors
{
    public async Task<VendorDetailsModel> CreateVendorAsync(VendorCreateModel request)
    {
        // create the thing to save in the database, save it(?) return a VendorDetailsModel
        // create insert statement, run it the database.
        var vendorToSave = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Url = request.Url,
            Contact = request.Contact,
            CreatedBy = "slime",
            CreatedOn = DateTimeOffset.UtcNow
        };
        session.Store(vendorToSave);
        await session.SaveChangesAsync();
        var response = new VendorDetailsModel(vendorToSave.Id, vendorToSave.Name, vendorToSave.Url, vendorToSave.Contact, vendorToSave.CreatedBy, vendorToSave.CreatedOn);
        return response;
    }

    public async Task<VendorDetailsModel?> GetVendorByIdAsync(Guid id, CancellationToken token)
    {
        var entity = await session.Query<VendorEntity>().Where(v => v.Id == id).SingleOrDefaultAsync();
        if (entity == null)
        {
            return null;
        } else
        {
            var response = new VendorDetailsModel(entity.Id, entity.Name, entity.Url, entity.Contact!, entity.CreatedBy, entity.CreatedOn);
            return response;
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
}