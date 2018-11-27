﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CityInfo.API
{
    public class Startup
    {
        //public static IConfigurationRoot Configuration;
        public static IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            env.ConfigureNLog("nlog.config");

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
            //Configuration = builder.Build();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(o => {
            //    if(o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

            //call this in case you need aspnet-user-authtype/aspnet-user-identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = @"Server=(localdb)\ProjectsV13;Database=CityInfoDB;Trusted_Connection=True;";
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();
            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages();
            app.UseMvc();

            //app.Run((context) =>
            //{
            //    throw new Exception("Example exception");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
