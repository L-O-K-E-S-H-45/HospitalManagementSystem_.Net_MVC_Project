using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.SqlConnectionObject;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //try
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .Build();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Error reading configuration: {ex.Message}");
        //    throw;
        //}

        builder.Services.AddTransient<IDoctorsRepository, DoctorRepository>();
        builder.Services.AddTransient<IDoctorBusiness, DoctorBusiness>();

        builder.Services.AddTransient<IPatientRepository, PatientRepository>();
        builder.Services.AddTransient<IPatientBusiness, PatientBusiness>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
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