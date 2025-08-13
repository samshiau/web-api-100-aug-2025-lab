using Alba;
using Shows.Api.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShow()
    {
        var postRequestModel = new ShowCreateRequest
        {
            Name = "43BIG SHOW",
            Description = "This is a test show",
            StreamingService = "Netflix"
        };
       fixture.FakeTime.Advance(TimeSpan.FromSeconds(5));
        var postResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(postRequestModel).ToUrl("/api/shows");
            _.StatusCodeShouldBeOk();
        });
        
        var postResponseBody = postResponse.ReadAsJson<ShowDetailsResponse>();
        Assert.NotNull(postResponseBody);
        var getAllResponse = await _host.Scenario(api =>
        {
            api.Get.Url("/api/shows");
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = getAllResponse.ReadAsJson<IReadOnlyList<ShowDetailsResponse>>();

        Assert.NotNull(getResponseBody);

        var firstShow = getResponseBody.First();

        Assert.Equal(firstShow.CreatedAt, postResponseBody.CreatedAt,TimeSpan.FromMilliseconds(5));
        Assert.True(firstShow.IsCloseEnoughTo(postResponseBody));
    }
    
}

public static class ShowComparer
{
    public static bool IsCloseEnoughTo(this ShowDetailsResponse me, ShowDetailsResponse other)
    {
        return me.Id == other.Id &&
            me.Name == other.Name &&
            me.Description == other.Description &&
            me.StreamingService == other.StreamingService;
    }
}