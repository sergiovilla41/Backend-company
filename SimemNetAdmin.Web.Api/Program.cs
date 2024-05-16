using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Clase inicializadora
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Compilador
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creacion de Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
