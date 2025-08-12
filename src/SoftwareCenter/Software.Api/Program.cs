

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build(); // The line in the sand, above this is configuring services.
                           // Below this is configuring middleware.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

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