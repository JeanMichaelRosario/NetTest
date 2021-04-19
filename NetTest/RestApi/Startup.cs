using Data.Common;
using Data.Interfaces;
using Data.SQLite;
using Data.SqlServer;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi
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
            services.AddControllers();
            services.AddScoped<ICurrencyService, CurrencyService>(x => new CurrencyService(Configuration.GetValue<string>("DollarUrl")));
			//services.AddScoped<IDataConnection, SQLite>(x => new SQLite(Configuration.GetValue<string>("Database:Sqlite")));
			services.AddScoped<IDataConnection, SqlServer>(x => new SqlServer(Configuration.GetValue<string>("Database:SQLServer")));
			services.AddScoped<ITransferService, TransferService>(x => 
                                            new TransferService(
                                                Configuration.GetSection("TransferLimits").Get<List<TransferLimit>>(),
                                                x.GetService<IDataConnection>(),
                                                x.GetService<ICurrencyService>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
