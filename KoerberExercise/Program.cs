using KoerberExercise.Data.Contexts;
using KoerberExercise.Filters;
using KoerberExercise.HealthChecks;
using KoerberExercise.Logic.Initializers;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(opts => opts.Filters.Add<ValidationExceptionFilter>())
    .AddNewtonsoftJson(options =>
    {
        var dateConverter = new IsoDateTimeConverter
        {
            DateTimeFormat = "dd-M-yyyy hh:mm:ss"// "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff"
        };
        options.SerializerSettings.Converters.Add(dateConverter);
    })
    .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHealthChecks()
    .AddCheck<DbAvailableHealthCheck>("Db Availability", null, new[] { "database", "sql" } )
    .AddCheck<LoadHealthCheck>("Load Response Time", null, new[] { "service" });

KoerberExercise.Logic.DIContainer.ServiceCollectionExtensions.Add(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health");

Initializers.Populate(app);

app.Run();
