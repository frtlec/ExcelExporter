using BlazorWebUI;
using BlazorWebUI.Bussines;
using BlazorWebUI.Components;
using BlazorWebUI.Databases.Mongo;
using BlazorWebUI.Databases.Mongo.DataAccess;
using BlazorWebUI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 1000;
    config.SnackbarConfiguration.HideTransitionDuration = 600;
    config.SnackbarConfiguration.ShowTransitionDuration = 50;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    config.SnackbarConfiguration.ClearAfterNavigation = true;
});
builder.Services.AddSingleton<ViewOptionStateContainer>();
builder.Services.AddSingleton<StateContainer>();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<TahtakaleIntegration>();
builder.Services.AddTransient<FollowedProductDataAccess>();
builder.Services.AddTransient<FollowedProductService>();
builder.Services.AddSingleton<IMongoDBSettings>(sp =>
  sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
builder.Services.Configure<MongoDBSettings>(
  builder.Configuration.GetSection(nameof(MongoDBSettings)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
