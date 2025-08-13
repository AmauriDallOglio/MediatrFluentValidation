using FluentValidation.AspNetCore;
using MediatrFluentValidation.Command;
using MediatrFluentValidation.Middleware;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MediatrFluentValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            // Registrar os serviços
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Adiciona o conversor para serializar enums como strings
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(fv =>
                {
                    // Registra os validadores a partir do assembly especificado
                    fv.RegisterValidatorsFromAssembly(Assembly.Load("MediatrFluentValidation"));
                });


            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InserirUsuarioCommandHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Program>();
                // Se desejar execução paralela entre handlers:
                // cfg.NotificationPublisher = new TaskWhenAllPublisher();
            });




            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
