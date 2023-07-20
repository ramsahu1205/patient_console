using doc_app_project;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.Web.Administration;
using Serilog;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;



string envName = "def_env";
string APP_POOL_ID = Environment.GetEnvironmentVariable("APP_POOL_ID");
string ASPNETCORE_URLS = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (!string.IsNullOrEmpty(ASPNETCORE_URLS))
{
   Uri u= new Uri(ASPNETCORE_URLS);
    envName = u.Host+":"+u.Port;
}

if (!string.IsNullOrEmpty(APP_POOL_ID))
{
    envName = APP_POOL_ID;
}

Console.WriteLine("envName "+envName);

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

var builder = WebApplication.CreateBuilder(args);

logger.LogInformation("Working with envirement==>"+envName);
//string path = builder.Configuration.GetSection(envName).Get<string>();

//DotEnv.Load(path);
//string static_path = EnvData.data[EnvCont.asset_path];
//string html_path = builder.Configuration.GetSection("html_path").Get<string>();
//IServerAddressesFeature addressFeature = null;
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(static_path),
//    RequestPath = "/assets/image"

//});

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(html_path),
//});






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
app.MapControllers();
//var server = app.Services.GetRequiredService<IServer>();
//var pi = server.GetType().GetProperty("path");

//app.MapGet("/", (HttpRequest request,HttpResponse response) => {
//    return response.SendFileAsync(html_path+"/index.html");
//});

app.MapGet("/proxy/env/data", (HttpRequest request) =>
{

    //if (EnvData.data.Count != 0)
    //{
    //    return EnvData.data;
    //}

    ////string port =request.Host.Port.ToString();
    //string domain = request.Host.Value;
    ////if (!string.IsNullOrEmpty(port))
    ////{
    ////    domain = domain+":"+port;
    ////}
    //Console.WriteLine("Domain==>"+ domain);
    //string path = builder.Configuration.GetSection(domain).Get<string>();
    //Console.WriteLine("Env Path==>"+path);
    //if (!string.IsNullOrEmpty(path))
    //{
    //     DotEnv.Load(path);
    //    string static_path = EnvData.data[EnvCont.asset_path];
    //    if (!string.IsNullOrEmpty(static_path))
    //    {

    //        // DotEnv.setupStaticFile(app,html_path,static_path);
    //    }

    //}
    return EnvData.data;

});

app.MapGet("/ram", (HttpRequest request) => {
    //string static_payh = EnvData.data[EnvCont.asset_path];
    //string[] dirList = Directory.GetDirectories(Path.Combine(html_path, "assets", "image"));
    //string[] fileList = Directory.GetFiles(Path.Combine(html_path, "assets", "image"));
    //Collection<string> d = new Collection<string>();
    //foreach(string f in dirList)
    //{
    //    d.Add(Path.GetDirectoryName(f));
    //}
    //foreach (string f in fileList)
    //{
    //    d.Add(Path.GetFileName(f));
    //}
    return envName;
});

app.Run();

//app.Start();

//var server1 = app.Services.GetService<IServer>();
//var addressFeature1 = server1.Features.Get<IServerAddressesFeature>();

//foreach (var address in addressFeature1.Addresses)
//{
//    Console.WriteLine("Kestrel is listening on address1: " + address);
//}


//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(html_path),
//    RequestPath="/assets"
//});

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider("D:\\alobha_doctor\\statics\\client1"),
//    RequestPath = "/assets/image"
//});

//app.WaitForShutdown();

//app.Start();


//app.WaitForShutdown();

