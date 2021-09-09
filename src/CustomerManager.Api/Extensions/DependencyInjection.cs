﻿using CustomerManager.Application.Services;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Domain.Services.Validator;
using CustomerManager.Infra.Context;
using CustomerManager.Infra.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace CustomerManager.Api.Extensions
{
    public static class DependencyInjection
    {

        public static void InjectDependencies(this IServiceCollection services, IConfiguration config)
        {
            //inject dbcontext
            var connectionString = config.GetConnectionString("postgres");
            services.AddDbContext<CustomerManagerContext>(options => options.UseNpgsql(connectionString));

            //Dependency Injection
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IValidator<Customer>, CustomerValidator>();
            services.AddRefitClient<IProductService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://challenge-api.luizalabs.com"));

        }
    }
}
