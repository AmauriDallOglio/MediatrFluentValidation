
using FluentValidation.AspNetCore;
using MediatrFluentValidation.Aplicacao.Command;
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
                    fv.RegisterValidatorsFromAssembly(Assembly.Load("MediatrFluentValidation.Aplicacao"));
                });


            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InserirUsuarioCommandHandler).Assembly));



            // Registrar MediatR e todos os handlers
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Adiciona suporte ao Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configurar o pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
