using Identity.Auth.Web;

await CreateHostBuilder(args)
    .Build()
    .RunAsync();
static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
