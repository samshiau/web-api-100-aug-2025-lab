
using System.Net.Http.Json;
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class ManagersCanAddVendors
{

    [Fact]
    [Trait("Category", "SystemsTest")]
    public async Task CanAddAVendor()
    {

        var vendorToPost = new VendorCreateModel
        {
            Name = "Microsoft",
            Contact = new PointOfContact
            {
                Name = "Satya",
                Email = "satya@microsoft.com",
                Phone = "555-1212"
            },
            Url = "https://microsoft.com"

        };
        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                //services.AddScoped<ICreateVendors, StubbedVendorCreator>();
            });
        });

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postResponseBody = await postResponse.ReadAsJsonAsync<VendorDetailsModel>();

        var location = postResponse.Context.Response.Headers.Location;
       
        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location!);
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = await getResponse.ReadAsJsonAsync<VendorDetailsModel>();
        Assert.NotNull(getResponse);

        Assert.Equal(postResponseBody, getResponseBody);

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