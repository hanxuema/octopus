using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.Extensions.Hosting;
using System;

namespace AWSServerless1
{
    public static class RedisConfiguration
    {
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                var redisConfig = configuration.GetSection("redis")
                    .Get<RedisOptions>();

                services.Configure<RedisOptions>(configuration.GetSection("redis"));
                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    Ssl = redisConfig.Ssl,
                    EndPoints = { redisConfig.RedisConnect },
                    Password = redisConfig.AuthToken
                };

                var conn = ConnectionMultiplexer.Connect(option);
                services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(option));
            }
            else
            {
                var redisConnct = Environment.GetEnvironmentVariable("redisConnct");
                Console.WriteLine($"redisConnct is {redisConnct}");

                var Env = Environment.GetEnvironmentVariable("Env");
                Console.WriteLine($"Env is {Env}");

                var ssl = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ssl"));
                Console.WriteLine($"ssl is {ssl}");

                var authToken = Environment.GetEnvironmentVariable("authToken") ?? "";

                Console.WriteLine($"authToken is {authToken}");

                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    Ssl = ssl,
                    EndPoints = { redisConnct },
                    Password = authToken
                };

                var conn = ConnectionMultiplexer.Connect(option);
                services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(option));
            }
        }
    }
}
