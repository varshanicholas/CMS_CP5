using CMS.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register repositories with connection string
             var connectionString = builder.Configuration.GetConnectionString("connectionStringDoctor");
            ////builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
            //builder.Services.AddSingleton<IAppointmentRepository>(provider => new AppointmentRepository(connectionString));
            //builder.Services.AddTransient<IConsultationRepository>(provider => new ConsultationRepository(connectionString));
            //builder.Services.AddTransient<IMedicineRepository>(provider => new MedicineRepository(connectionString));
            //builder.Services.AddTransient<ILabTestRepository>(provider => new LabTestRepository(connectionString));

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

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Doctor}/{action=LandingPageDoctor}/{id?}");


            //test purpose later comment this
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=TestLogin}/{action=Index}/{id?}");

            app.MapControllerRoute(name: "consultation", pattern: "{controller=Consultation}/{action=StartConsultation}/{appointmentId?}");

            app.Run();
        }
    }
}
