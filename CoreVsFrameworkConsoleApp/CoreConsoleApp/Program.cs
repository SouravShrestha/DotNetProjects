using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Install-Package Microsoft.Extensions.Configuration
            Install-Package Microsoft.Extensions.Configuration.Json
            Install-Package Microsoft.Extensions.Configuration.EnvironmentVariables 
            Install-Package Microsoft.Extensions.Configuration.Binder 
            */

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables().Build();
            //The default JsonConfigurationProvider loads configuration in the following order: appsettings.json, appsettings.Environment.json

            //Create appsettings.json - Properties - Copy to output directory: Copy if newer

            //Accessing config entries
            var connectionString = configuration["ConnectionString"];
            var fullName = configuration["MyDetails:Firstname"] + " " + configuration["MyDetails:Lastname"];
            Console.WriteLine("Direct: {0}: {1}", fullName, connectionString);

            //Mapping the whole config
            var cfg = configuration.Get<AppConfig>();
            connectionString = cfg.ConnectionString;
            fullName = cfg.MyDetails.Firstname + " " + cfg.MyDetails.Lastname;
            Console.WriteLine("Mapping the whole config: {0}: {1}", fullName, connectionString);

            //Mapping a Section
            var myDetails = new MyDetail();
            configuration.GetSection("MyDetails").Bind(myDetails);
            Console.WriteLine("Mapping a Section: {0}", myDetails.Firstname);

            //Making it Generic
            cfg = InitOptions<AppConfig>();
            connectionString = cfg.ConnectionString;
            fullName = cfg.MyDetails.Firstname + " " + cfg.MyDetails.Lastname;
            Console.WriteLine("Direct: {0}: {1}", fullName, connectionString);
        }

        private static T InitOptions<T>() where T : new()
        {
            var config = InitConfig();
            return config.Get<T>();
        }

        //InitConfig for making it Generic
        private static IConfigurationRoot InitConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }

    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public MyDetail MyDetails { get; set; }
    }

    public class MyDetail
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
