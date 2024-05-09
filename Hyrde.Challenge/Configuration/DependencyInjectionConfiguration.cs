﻿using Hyrde.Challenge.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Hyrde.Challenge.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureApiHttpClients(this IServiceCollection _services, IConfiguration configuration)
        {
            var baseUrl = configuration.GetValue<string>("BaseUrl");
            var apiKey = configuration.GetValue<string>("ApiKey");
            _services.AddHttpClient<IWeatherService, WeatherService>(client => {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("api-key-header-name", apiKey);
            });
            return _services;
        }
    }
}
