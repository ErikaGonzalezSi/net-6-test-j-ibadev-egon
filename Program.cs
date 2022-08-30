using net_6_test_j_ibadev_egon_pr.Models;
using net_6_test_j_ibadev_egon_pr.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Handle model errors and convert in format standard
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.InvalidModelStateResponseFactory = context =>
        {
            var Error = context.ModelState.Keys.Select(k => k).First();
            var ErrorDescription = context.ModelState[Error]?.Errors.Select(e => e.ErrorMessage).First();
            var TechnicalError = "Error validate data model";
            ErrorMessage FormatMessage = new ErrorMessage
            {
                Error = Error,
                ErrorDescription = ErrorDescription,
                TechnicalError = TechnicalError

            };

            return new BadRequestObjectResult(FormatMessage);
        };
    });

//builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Database connection
builder.Services.AddDbContext<EERPContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Handle technical error and convert in format standard
app.UseMiddleware<ErrorHand>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
