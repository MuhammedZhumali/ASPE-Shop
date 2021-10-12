using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using E_Shop.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Shop
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      string connection = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContextPool<UsersContext>(options => options.UseMySql(connection));
      services.AddControllersWithViews();
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
              builder
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
            });
      });


      services.AddControllers();

      services.AddApiVersions();

      services.AddSwagger();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sapsan v1"));

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors("AllowAllOrigins");

      app.UseAuthentication();    // аутентификация
      app.UseAuthorization();     // авторизация

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
