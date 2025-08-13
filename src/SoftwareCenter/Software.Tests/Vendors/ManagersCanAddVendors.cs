
using System.Net.Http.Json;
using Alba;
using Alba.Security;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

[Trait("Category", "SystemsTest")]
public class ManagersCanAddVendors
{

    [Fact]

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
        }, new AuthenticationStub());

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

    [Fact]
    public async Task InvalidVendorRequestsReturnBadRequest()
    {
        var host = await AlbaHost.For<Program>((_) => { }, new AuthenticationStub());

        var badRequest = new VendorCreateModel { Name = "", Contact = null!, Url = "" };

        await host.Scenario(api =>
        {
            api.Post.Json(badRequest).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });

    }

    [Fact]
    public async Task UnathenticatedGetsA400()
    {
        var host = await AlbaHost.For<Program>();

        var badRequest = new VendorCreateModel { Name = "", Contact = null!, Url = "" };

        await host.Scenario(api =>
        {
            api.Post.Json(badRequest).ToUrl("/vendors");
            api.StatusCodeShouldBe(401);
        });
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