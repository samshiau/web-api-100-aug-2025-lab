using Microsoft.AspNetCore.Mvc;

namespace Software.Api.Vendors;

public class VendorsController : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] VendorCreateModel request,
        CancellationToken token)
    {
        return Ok(request);
    }
}

/*{
  "name": "Microsoft",
  "url": "https://microsoft.com",
  "contact": {
    "name": "Satya",
    "email": "Satya@microsoft.com",
    "phone": "888 555-1212"
  }
}*/

public record PointOfContact(string Name, string Email, string Phone);
public record VendorCreateModel(string Name, string Url, PointOfContact Contact);