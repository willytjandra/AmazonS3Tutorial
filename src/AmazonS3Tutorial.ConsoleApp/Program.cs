using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using AmazonS3Tutorial.ConsoleApp.AmazonS3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AmazonS3Tutorial.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHost(args);

            var awsConfig = host.Services.GetService<AwsConfig>();
            var s3Client = host.Services.GetService<IAmazonS3>();

            var transferUtility = new TransferUtility(s3Client);

            using var stream = File.OpenRead("hello-world.txt");
            await transferUtility.UploadAsync(stream, awsConfig.BucketName, $"{DateTime.Now:yyyyMMdd_hhmmss}_hello-world.txt");

            Console.WriteLine("Hello World!");
        }

        private static IHost CreateHost(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices)
            .Build();

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var awsConfig = context.Configuration.GetSection("AwsConfig").Get<AwsConfig>();
            services.AddSingleton(awsConfig);

            services.AddSingleton<IAmazonS3>(serviceProvider =>
            {
                var s3Config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(awsConfig.RegionName)
                };

                var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (hostEnvironment.IsDevelopment())
                {
                    // For development environment, we use localstack
                    s3Config.ForcePathStyle = true;
                    s3Config.ServiceURL = awsConfig.LocalStackServiceUrl;
                }

                return new AmazonS3Client(awsConfig.AwsAccessKey, awsConfig.AwsSecretKey, s3Config);
            });
        }
    }
}
