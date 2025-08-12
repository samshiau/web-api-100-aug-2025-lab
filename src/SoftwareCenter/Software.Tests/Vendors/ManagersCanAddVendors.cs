
using System.Net.Http.Json;
using Alba;

using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class ManagersCanAddVendors
{

    [Fact]
    public async Task CanAddAVendor()
    {

        var vendorToPost = new VendorCreateModel("Microsoft", "https://www.microsoft.com", new PointOfContact("Satya", "satya@microsoft.com", "800 the-boss"));


        var host = await AlbaHost.For<Program>();

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });

        var postResponseBody = await postResponse.ReadAsJsonAsync<VendorCreateModel>();
        Assert.NotNull(postResponseBody);
        Assert.Equal(vendorToPost, postResponseBody);
        // query the database and see if the thing was saved.

        // when I do a GET /vendors, this new vendor shows up there.
    }
}
