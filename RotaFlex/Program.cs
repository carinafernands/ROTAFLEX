using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using RotaFlex.Datas;

internal class Program
{
    private static void Main(string[] args)
    {
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        var connStr = Environment.GetEnvironmentVariable("RailwayConnectString");

        if (string.IsNullOrEmpty(connStr))
        {
            throw new Exception("A string de conexão não foi encontrada no arquivo .env.");
        }

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                connStr,
                new MySqlServerVersion(new Version(8, 0, 36)),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                )
            );
        builder.Services.AddScoped<SeedingService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using (var scope = app.Services.CreateScope())
        {
            var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
            seedingService.Seed();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}