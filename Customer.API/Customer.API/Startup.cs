using CustomerAPI.Core.Data;
using CustomerAPI.Core.Data.Interface;
using CustomerAPI.Core.Repository;
using CustomerAPI.Core.Repository.Interface;
using Microsoft.AspNetCore.Builder;

namespace CustomerAPI
{
    public class Startup
    {
        public IConfiguration configuration
        {
            get;set;
        }
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //This method gets called by runtime. Register your DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICustomerStore, CustomerStore>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.Run();
        }
    }
}
