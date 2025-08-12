using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Software.Api.Vendors;


public class VendorsController : ControllerBase
{
    [HttpPost("/vendors")] // POST to a collection resource. 
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] VendorCreateModel request,
        [FromServices] ICreateVendors vendorCreator,
        [FromServices] IValidator<VendorCreateModel> validator
        )
    {

        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest();
        }
        //// Validation
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}

        // Validate the incoming request, if it isn't valid, send a 400 response.
        // If it is valid - "store this thing" "Side effect"
        // return a success result, and if you can, a copy of the thing you "created"


        VendorDetailsModel response = await vendorCreator.CreateVendorAsync(request);

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

public record PointOfContact
{
   
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}

public class PointOfContactValidator : AbstractValidator<PointOfContact>
{
    public PointOfContactValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}

public record VendorCreateModel
{

    public required string Name { get; init; } = string.Empty;
    public required string Url { get; init; } = string.Empty;
    public required PointOfContact? Contact { get; init; }
    public VendorEntity MapToEntity(Guid id, string createdBy)
    {
        return new VendorEntity
        {
            Id = id,
            Contact = Contact,
            Name = Name,
            CreatedBy = createdBy,
            CreatedOn = DateTimeOffset.UtcNow,
            Url = Url,


        };
    }
};

public class VendorCreateModelValidator : AbstractValidator<VendorCreateModel>
{
    public VendorCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(v => v.Url).NotEmpty();
        RuleFor(v => v.Contact).SetValidator(validator: new PointOfContactValidator());
    }
}

public record VendorDetailsModel(Guid Id, string Name, string Url, PointOfContact Contact, string CreatedBy, DateTimeOffset CreatedOn);