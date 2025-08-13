

using FluentValidation;
using Marten;
using Software.Api.Vendors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddControllers();

builder.Services.AddAuthorizationBuilder().AddPolicy("SoftwareCenterManager", pol =>
{
    pol.RequireRole("SoftwareCenter");
    pol.RequireRole("Manager");
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("software") ?? throw new Exception("No Connection String Found In Environment");

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
}).UseLightweightSessions();

// an API, a "scoped" service means "use the same one for the entire request/response"
builder.Services.AddScoped<ICreateVendors, MartenVendorData>();
builder.Services.AddScoped<ILookupVendors, MartenVendorData>();
builder.Services.AddScoped<IValidator<VendorCreateModel>, VendorCreateModelValidator>();

var app = builder.Build(); // The line in the sand, above this is configuring services.
                           // Below this is configuring middleware.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
// Route Table:
// POST /vendors -> Create the Vendor Controllerr, and Call the AddVendorAsync Method.

app.MapControllers(); // before we run the application, go find all the "controllers" 

// "Minimal APIS"
app.MapGet("/status", () =>
{
    return Results.Ok(new { Status = "Good", Evaluated = DateTimeOffset.Now });
});

app.Run(); // an endless while loop, basically. it "blocks", keeps running here forever, waiting for requests.


public partial class Program;