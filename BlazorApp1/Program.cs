using BlazorApp1.Data;
using BlazorApp1.Hubs;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddHostedService<ChatHubService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapHub<ChatHub>("/chathub");
//app.Use(async (context, next) =>
//{
//    var hubContext = context.RequestServices.GetRequiredService<IHubContext<ChatHub>>();
//    var service = context.RequestServices.GetRequiredService<ChatHubService>();
//    service.SetHubContext(hubContext);
//    if (next != null)
//    {
//        await next.Invoke();
//    }
//});

app.MapFallbackToPage("/_Host");

app.Run();
