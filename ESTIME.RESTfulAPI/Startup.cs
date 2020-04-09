using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ESTIME.BusinessLibrary;

namespace ESTIME.RESTfulAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add the configuration object 
            services.AddSingleton<IConfiguration>(Configuration);

            //Register and configure EstimeContext
            //var connStr = Configuration.GetConnectionString("EstimeDb");
            //services.AddDbContext<DAL.EstimeEntity.EstimeContext>(options => options.UseSqlServer(connStr, providerOptions => providerOptions.CommandTimeout(60))
            //                                                                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            
            //Add business managers
            services.AddTransient<CodeSetManager>();
            services.AddTransient<DataLoaderManager>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
