using Microsoft.OpenApi.Models;
using Ordering.Infrastructure;
using Ordering.Application;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Ordering.API
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
            //try
            //{
            //    DbConnectionStringBuilder csb = new DbConnectionStringBuilder();
            //    csb.ConnectionString = "Data Source=LAPTOP-76ONP6BR;Initial Catalog=OrderDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"; // throws

            //    using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
            //    {
            //        conn.Open(); // throws if invalid
            //    }

            //    //string provider = "System.Data.SqlClient"; // for example
            //    //DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            //    //using (DbConnection conn = factory.CreateConnection())
            //    //{
            //    //    conn.ConnectionString = cs;
            //    //    conn.Open();
            //    //}
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);

            // MassTransit-RabbitMQ Configuration
            //services.AddMassTransit(config => {

            //    config.AddConsumer<BasketCheckoutConsumer>();

            //    config.UsingRabbitMq((ctx, cfg) => {
            //        cfg.Host(Configuration["EventBusSettings:HostAddress"]);

            //        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
            //        {
            //            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            //        });
            //    });
            //});
            //services.AddMassTransitHostedService();

            // General Configuration
            services.AddAutoMapper(typeof(Startup));
            //services.AddScoped<BasketCheckoutConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
