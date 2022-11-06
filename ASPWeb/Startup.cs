using AuthTutorial.Auth.Common;
using System.Security.Cryptography.X509Certificates;

namespace ASPWeb
{
    public class Startup
    {
        // ��������� �������� � ����������� ��� ����� �����������
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

            // ������ ��� ��������� ��������� ��������� ����������� Cors
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
                // ������ ����� ������� ��� ���������� �� �������
                // � ��������� �� ��� �������������
                endpoints.MapControllers();

            });
        
        }
    }

}