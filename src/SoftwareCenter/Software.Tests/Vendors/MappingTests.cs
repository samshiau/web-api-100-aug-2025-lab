
using System.ComponentModel;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class MappingTests
{

    [Fact]
    [Trait("Category", "Unit")]
    public void CanMapVendorCreateToEntity()
    {
        var model = new VendorCreateModel("X", "Y", new PointOfContact("Joe", "joe@email.com", "555-1212"));

        var id = Guid.NewGuid();
        var mapped = model.MapToEntity(id, "jeff");

        Assert.NotNull(mapped);
        Assert.Equal(id, mapped.Id);
        Assert.Equal(model.Url, mapped.Url);
        // etc etc.
    }
}
