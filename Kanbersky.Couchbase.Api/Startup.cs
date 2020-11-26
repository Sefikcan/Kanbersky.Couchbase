using AutoMapper;
using Couchbase.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kanbersky.Couchbase.Core.DataAccess.Abstract.Couchbase;
using Kanbersky.Couchbase.Infrastructure.Concrete.Couchbase;
using Kanbersky.Couchbase.Services.Abstract;
using Kanbersky.Couchbase.Services.Concrete;
using Kanbersky.Couchbase.Services.DTO.Request;
using Kanbersky.Couchbase.Services.Mappings.AutoMapper;
using Kanbersky.Couchbase.Services.ValidationRules.FluentValidations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Kanbersky.Couchbase.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.AddCouchbase(Configuration.GetSection("Couchbase")); //service register
            services.AddScoped(typeof(ICouchbaseRepository<>), typeof(CouchbaseRepository<>));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddTransient<IValidator<GetPageableCustomerRequestModel>, GetPageableCustomerValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kanbersky Couchbase Microservice",
                    Version = "v1",
                    Description = "An API to perform Customer operations",
                    Contact = new OpenApiContact
                    {
                        Email = "sefikcan.kanber@gmail.com.tr",
                        Name = "Þefik Can Kanber"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="appLifetime"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kanbersky Couchbase Microservices v1");
            });

            //.Net Core down olması durumunda Couchbase'i dispose eder
            appLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
        }
    }
}
