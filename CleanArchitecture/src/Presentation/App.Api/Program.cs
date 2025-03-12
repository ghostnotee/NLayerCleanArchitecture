using App.Api.ExceptionHandlers;
using App.Api.Extensions;
using App.Api.Filters;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithFiltersExtension();
builder.Services.AddDocumentationExtension();
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddRepositories(builder.Configuration).AddServices();
builder.Services.AddScoped(typeof(NotFoundFilter<,>));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseScalarExtension();

app.UseExceptionHandler(applicationBuilder => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();