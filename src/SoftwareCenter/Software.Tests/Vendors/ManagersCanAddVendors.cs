
using System.Net.Http.Json;
using Alba;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class ManagersCanAddVendors
{

    [Fact]
    public async Task CanAddAVendor()
    {
        //var client = new HttpClient();
        //client.BaseAddress = new Uri("http://localhost:1337");

        var vendorToPost = new VendorCreateModel("Microsoft", "https://www.microsoft.com", new PointOfContact("Satya", "satya@microsoft.com", "800 the-boss"));

        //var response = await client.PostAsJsonAsync("/vendors", vendorToPost);

        //response.EnsureSuccessStatusCode(); // blow up if we get anything other than 200-299

        //Assert.NotNull(response);

        //Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        //var responseBody = await response.Content.ReadFromJsonAsync<VendorCreateModel>();

        //Assert.NotNull(responseBody);

        //Assert.Equal(vendorToPost, responseBody);

        var host = await AlbaHost.For<Program>();

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });

        var postResponseBody = await postResponse.ReadAsJsonAsync<VendorCreateModel>();
        Assert.NotNull(postResponseBody);
        Assert.Equal(vendorToPost, postResponseBody);
    }
}
