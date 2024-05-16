using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SimemNetAdmin.Application.Services.BackgroundTaskService;
using SimemNetAdmin.Infra.IoC;
using SimemNetAdmin.Transversal.Interfaces;
using SimemNetAdmin.Transversal.KeyVault;
using SimemNetAdmin.Transversal.SendNotifications;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Inyección configuración
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// configuración servicio
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddControllers();
            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Administrador Simem",
                    Version = "v1",
                    Description = "Open Api Administrador Simem",
                    Contact = new OpenApiContact
                    {
                        Name = "Administrador Simem",
                        Email = "admin@simem.com",
                    }
                });
            });

            KeyVaultTypes[] enumValues = (KeyVaultTypes[])Enum.GetValues(typeof(KeyVaultTypes));
            byte[] decryted;

            decryted = Convert.FromBase64String(GetKeyValue("clientId"));
            string clientId = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(GetKeyValue("clientSecret"));
            string clientSecret = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(GetKeyValue("tenantId"));
            string tenantId = System.Text.Encoding.Unicode.GetString(decryted);

            var vaultUri = new Uri(GetKeyValue("AzureKeyVaultUri"));

            ClientSecretCredential credential = new(tenantId, clientId, clientSecret);

            var client = new SecretClient(vaultUri, credential);

            foreach (var keyName in enumValues)
            {
                string secret = KeyVaultManager.GetSettingValue(keyName);


                if (!KeyVaultManager.IsPipelineVariableActive())
                {
                    secret = client.GetSecret(secret).Value.Value;
                }

                KeyVaultManager.SetSecretValue(keyName.ToString(), secret);
            }

            services.AddSingleton<IEmailSender>(e => new EmailSender());
            services.AddHostedService<BackgroundTaskService>();
       
            RegisterServices(services);
        }

        /// <summary>
        /// Obtener valores variables de entorno
        /// </summary>
        public static string GetKeyValue(string value)
        {

            string? key = Environment.GetEnvironmentVariable(value);

            return key ?? "";
        }

        /// <summary>
        /// Configuración de inicio
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Habilitar Swagger en el entorno de desarrollo
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ADMINISTRADOR SIMEM API");
                });
            }
            else
            {
                // Configuraciones para entorno de producción...
            }
           


            app.UseRouting();

            app.UseCors("AllowAngularApp");

            app.UseMiddleware<AuthMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });





            app.Map("/backend-files", _app =>
            {
                _app.Run(async context =>
                {
                    // Create my object
                    var _object = new
                    {
                        Title = "Your web app is running and waiting for your content",
                        Description = "Your web app is live, but we don’t have your content yet. " +
                        "If you’ve already deployed, it could take up to 5 minutes for your content to show up, so come back soon.",
                        Code = "Supporting Node.js, Java, .NET and more"
                    };

                    // Transform it to JSON object
                    string jsonData = JsonConvert.SerializeObject(_object);
                    await context.Response.WriteAsync(jsonData);
                });
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }

    
}
