namespace API.Configurations;

public static class AppPipelineConfiguration
{
    public static void ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
    }

    public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger(); // Генерация документации Swagger
            app.UseSwaggerUI(); // Отображение UI для Swagger

        }
    }

    public static void ConfigureEndpoints(this IApplicationBuilder app)
    {

        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllers();
            //endpoints.MapControllerRoute(
            //    name: "MyArea",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            //endpoints.MapAreaControllerRoute(
            //       name: "Admin",
            //       areaName: "Admin",
            //       pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

            //endpoints.MapAreaControllerRoute(
            //    name: "Admin",
            //    areaName: "Admin",
            //    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            //endpoints.MapAreaControllerRoute(
            //    name: "Teacher",
            //    areaName: "Teacher",
            //    pattern: "teacher/{controller=Home}/{action=Index}/{id?}");
            //endpoints.MapAreaControllerRoute(
            //    name: "Student",
            //    areaName: "Student",
            //    pattern: "student/{controller=Home}/{action=Index}/{id?}");
            //endpoints.MapDefaultControllerRoute();


        });


    }
}
