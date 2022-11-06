using AuthTutorial.Auth.Common;
using System.Security.Cryptography.X509Certificates;

namespace ASPWeb
{
    public class Startup
    {
        // ƒобавл€ем свойства и заинъектить его через конструктор
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllers();
            
            var authOptionsConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfiguration);

            // данный код полностью отключает созданные ограничени€ Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called be the runtime.
        // Use this method to configure the HTTP request pipeline

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                // данный метод находит все контролеры по проекту
                // и добавл€ет их дл€ использовани€
                endpoints.MapControllers();

            });
        
        }
    }

}