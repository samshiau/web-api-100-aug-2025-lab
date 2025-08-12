
using System.Net.Http.Json;
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class ManagersCanAddVendors
{

    [Fact]
    public async Task CanAddAVendor()
    {

        var vendorToPost = new VendorCreateModel("Microsoft", "https://www.microsoft.com", new PointOfContact("Satya", "satya@microsoft.com", "800 the-boss"));


        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                services.AddScoped<ICreateVendors, StubbedVendorCreator>();
            });
        });

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });

        var postResponseBody = await postResponse.ReadAsJsonAsync<VendorDetailsModel>();
        Assert.NotNull(postResponseBody);
        // Does it have an ID.
        Assert.Equal(Guid.Parse("c904c645-ed16-49a6-b3f6-e32e8662c400"), postResponseBody.Id);
        Assert.Equal("sue@aol.com", postResponseBody.CreatedBy);
        /// etc. etc.
        /// 

        // query the database and see if the thing was saved.

        // when I do a GET /vendors, this new vendor shows up there.
    }
}


public class StubbedVendorCreator : ICreateVendors
{
    public Task<VendorDetailsModel> CreateVendorAsync(VendorCreateModel request)
    {
        var fake = new VendorDetailsModel(Guid.Parse("c904c645-ed16-49a6-b3f6-e32e8662c400"), request.Name, request.Url, request.Contact, "sue@aol.com", DateTimeOffset.UtcNow);
        return Task.FromResult(fake);
    }
}