using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PastebinClone.Configuration;
using PastebinClone.Formatters;
using PastebinClone.Formatters.Interfaces;
using PastebinClone.Queries;
using PastebinClone.Queries.Interfaces;
using PastebinClone.Services;
using PastebinClone.Services.Interfaces;

namespace PastebinClone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DirectoryLocation>(options => Configuration.GetSection("Directories").Bind(options));
            services.AddControllers();
            services.AddScoped<IFileDumpService, FileDumpService>();
            services.AddScoped<IFileCoordinator, FileCoordinator>();
            services.AddSingleton<IHtmlFormatter, HtmlFormatter>();
            services.AddScoped<IDirectoryQuery, DirectoryQuery>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            // app.UseFileServer(new FileServerOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine("/home/vic/Desktop/temp/"))
            // });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}