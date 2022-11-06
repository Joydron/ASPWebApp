using AuthTutorial.Auth.Common;
using AuthTutorial.Resourse.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASPWeb;


    public class Startup
    {
        // добавляем конфигурацию
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

        // данный код конфигурирует сервис работы с json webтокенами
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // разрешает валидировать прешедший по http а не https
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                // указывает будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                // строка предоствляющая издателя
                ValidIssuer = authOptions.Issuer,

                // указывает будет ли валидироваться потребитель токена
                ValidateAudience = true,
                // установка потребителя токена
                ValidAudience = authOptions.Audience,
                
                // будет ли валидироваться время сущестовования токена
                ValidateLifetime = true,

                // установка ключа безопасности
                IssuerSigningKey = authOptions.GetSymmetricSecutiryKey(), //HS256
                // будет ли валидироваться ключ безопасности
                ValidateIssuerSigningKey = true,
            };
        });


        // данный код полностью отключает созданные ограничения Cors

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

        // регистрирует каталог книг как синглтон
        services.AddSingleton(new BookStore());
    }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            // ауи-ция и авторизация в пайплайм
            app.UseAuthentication();
            app.UseAuthorization();

        // Конфигурация пайплайм для использования контролеров    
        app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }
    }

