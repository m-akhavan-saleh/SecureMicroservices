using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // اضافه کردن سرویس مربوط به کنترلر + نما

//builder.Services.AddIdentityServer();   // اضافه کردن به لیست سرویس ها

builder.Services.AddIdentityServer() // اضافه کردن به لیست سرویس به همراه انجام تنظیمات لازم
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseStaticFiles(); // فایل های استاتیک
app.UseRouting(); // مسیریابی
app.UseIdentityServer(); // استفاده از سرویس در برنامه
app.UseAuthorization(); // انجام عملیات احراز هویت

app.UseEndpoints(endpoints =>
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapDefaultControllerRoute(); // آدرس دهی پیش فرض مربوط به کنترلرها

//    //endpoints.MapGet("/", async context =>
//    //{
//    //    await context.Response.WriteAsync("Hello World!");
//    //});
//});

//app.MapGet("/", () => "Hello World!");

app.Run();