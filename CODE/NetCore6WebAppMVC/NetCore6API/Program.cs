using BusinessLayer.Interface;
using DataAccessLayer;

namespace NetCore6API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            #region Person Service
            //Add services to Person for Dependency Injection

            //RC-NOTE: Choose one of the following services below depending on the application needs; 

            ///Transient - A new instance is provided every time an instance is requested whether the scope is within the scope of the same http request or across different http requests.
            //builder.Services.AddTransient<IPersonService, PersonService>();

            ///Scoped - We get the same instance within the scope of a given http request.
            builder.Services.AddScoped<IPersonService, PersonService>();

            ///Singleton - There is only a single instance. An instance is created, when service is first requested and that instance will be used by all subsequent http request throughout the application.
            //builder.Services.AddSingleton<IPersonService, PersonService>();

            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}