using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Steeltoe.Extensions.Configuration.CloudFoundry;
//using Steeltoe.Management.CloudFoundry;
using Newtonsoft.Json.Linq;
using insignia.Models;

namespace insignia
{
    public class Startup
    {
        private static readonly string BOUND_SERVICES_ENV_VARIABLE_NAME = "VCAP_SERVICES";

        public static JObject BoundServices
        {
            get { return JObject.Parse(Environment.GetEnvironmentVariable(BOUND_SERVICES_ENV_VARIABLE_NAME)); }
        }

        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.LogError("########################################################\n" +
                            "########################################################\n" +
                            BoundServices +
                            "########################################################\n" +
                            "########################################################\n");
            if (BoundServices.GetValue("cleardb") != null)
            {
                var connectionString =
                        "server=" + BoundServices["cleardb"][0]["credentials"]["hostname"].ToString() + ";" +
                        "database=" + BoundServices["cleardb"][0]["credentials"]["name"].ToString() + ";" +
                        "user=" + BoundServices["cleardb"][0]["credentials"]["username"].ToString() + ";" +
                        "password=" + BoundServices["cleardb"][0]["credentials"]["password"].ToString();
                Configuration["mysql:client:ConnectionString"] = connectionString;
                Logger.LogError("@@@@#######################################################\n" +
                                "@@@@#######################################################\n" +
                                connectionString +
                                "@@@@########################################################\n" +
                                "@@@@########################################################\n");
            }
            services.AddOptions();
            services.ConfigureCloudFoundryService<MySqlServiceOption>(Configuration, "cleardb");
            services.AddDbContext<AirplaneContext>(options => options.UseMySql(Configuration));
            //services.AddMySqlHealthContributor(Configuration);
            services.GetEnumerator();
            //services.AddCloudFoundryActuators(Configuration);

            // Add framework services.
            services.AddMvc();

            services.AddOptions();
            services.ConfigureCloudFoundryService<MySqlServiceOption>(Configuration, "cleardb");
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseCloudFoundryActuators();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Airplane}/{action=Index}/{id?}");
            });

        }// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void OldConfigure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            // app.UseWelcomePage();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
        }
    }


}

public class MySqlServiceOption : AbstractServiceOptions
{
    public MySqlServiceOption() { }
    public MySqlCredentials Credentials { get; set; }
}

public class MySqlCredentials
{
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Uri { get; set; }
    public string JdbcUrl { get; set; }
}
