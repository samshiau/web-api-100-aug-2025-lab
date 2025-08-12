
using Software.Api.Vendors;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;

namespace Software.Tests.Vendors;

[Trait("Category", "Unit")]
public class VendorCreateValidationTests
{
    [Theory]
    [MemberData(nameof(ValidExamples))]
    public void ValidVendorCreateRequests(VendorCreateModel model)
    {
        var validation = new VendorCreateModelValidator();
        var validations = validation.TestValidate(model);
        Assert.True(validations.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidExamples))]
    public void InvalidVendorCreateRequsts(VendorCreateModel model)
    {
        var validation = new VendorCreateModelValidator();
        var validations = validation.TestValidate(model);
        Assert.False(validations.IsValid);
    }


    //[Theory]
    //public void InvalidVendorCreateRequests(VendorCreateModel model)
    //{

    //}

    public static IEnumerable<object[]> ValidExamples() => new[]
    {
        new object[] {
            new VendorCreateModel {Name = "Joseph", Contact = new PointOfContact { Name="Joe"}, Url= "https://dog.com"}
    }
        };

    public static IEnumerable<object[]> InvalidExamples() => new[]
   {
        new object[] {
            new VendorCreateModel {Name = "", Contact = new PointOfContact {}, Url= "https://dog.com"}
    }
        };

}
