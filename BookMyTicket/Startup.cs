using System;
using System.IO;
using System.Reflection;
using System.Text;
using BookMyTicket.DataAccess;
using BookMyTicket.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Owin;
using BookMyTicket.Authorization;
using BookMyTicket.Helpers;
using BookMyTicket.Services;

//[assembly: OwinStartup(typeof(BookMyTicket.Startup))]
namespace BookMyTicket
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "ChatBox API",
					Description = "Trying to create chatbox api",
					Contact = new OpenApiContact
					{
						Name = "Somela Sanjeevareddy",
						Email = "somelasanjeevareddy@yahoo.com",
					}
				});
			});
			services.AddDbContext<LinqDbContext>(x => x.UseSqlServer(Configuration.GetSection("DatabaseConnectionString:ConnectionString").Value));
			RegisterServices(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger(x =>
			{
				x.SerializeAsV2 = true;
			});
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				x.RoutePrefix = string.Empty;
			});
			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});

			// global cors policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			// global error handler
			app.UseMiddleware<ErrorHandlerMiddleware>();

			// custom jwt auth middleware
			app.UseMiddleware<JwtMiddleware>();

		}

		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IJwtUtils, JwtUtils>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRegisterService, RegisterService>();
		}
	}
}
