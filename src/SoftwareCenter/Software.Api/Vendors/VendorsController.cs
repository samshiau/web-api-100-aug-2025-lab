using Microsoft.AspNetCore.Mvc;

namespace Software.Api.Vendors;

public class VendorsController : ControllerBase
{
    [HttpPost("/vendors")] // POST to a collection resource. 
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] VendorCreateModel request,
        [FromServices] ICreateVendors vendorCreator
        )
    {
        // Validation
        // Validate the incoming request, if it isn't valid, send a 400 response.
        // If it is valid - "store this thing" "Side effect"
        // return a success result, and if you can, a copy of the thing you "created"

   
        VendorDetailsModel response = await vendorCreator.CreateVendorAsync(request);

        // consider a 201 status code - which "created" - and a copy of the thing you just created.
        return Created($"/vendors/{response.Id}", response);
    }


    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync([FromRoute] Guid id,

        [FromServices] ILookupVendors vendorLookup,
        CancellationToken token )
    {

       VendorDetailsModel? response = await vendorLookup.GetVendorByIdAsync(id, token);
        if(response is null)
        {
            return NotFound();
        } else
        {
            return Ok(response);
        }
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

public record VendorDetailsModel(Guid Id, string Name, string Url, PointOfContact Contact, string CreatedBy, DateTimeOffset CreatedOn);