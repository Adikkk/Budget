using Budget.Domain;
using Budget.Domain.Expenses;
using Budget.Domain.Incomes;
using Budget.Infrastructure.IoC;
using Budget.Infrastructure.Persistence;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Budget.Domain.Shared;

namespace Budget.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddScoped<ValidationNotificationHandler>();
            services.AddSingleton(Configuration.Get<BudgetConfiguration>());

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });

            var sqlConnString = Configuration.GetConnectionString("SqlServerConnectionString");

            services
                .AddDbContext<WriteDbContext>(options =>
                options.UseSqlServer(sqlConnString,
                b => b.MigrationsAssembly("Budget.Infrastructure")));

            services
                .AddHealthChecks()
                .AddSqlServer(sqlConnString);
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CommandModule());
            builder.RegisterModule(new EventModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new QueryModule());
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ExpenseCreatedEvent>();
            eventBus.Subscribe<IncomeCreatedEvent>();
        }


        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Budget API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.EnvironmentName == "Docker")
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<WriteDbContext>();
                    context.Database.Migrate();
                }
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });

            ConfigureEventBus(app);
        }
    }
}