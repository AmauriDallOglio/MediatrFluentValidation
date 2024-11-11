using MediatrFluentValidation.Util;
using System.Net;
using System.Text.Json;

namespace MediatrFluentValidation.Middleware
{
    public class ErrorHandlingMiddleware
    {


        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly string _logFilePath = "logs/error_log.txt";

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponseBody = context.Response.Body;

            try
            {
                using var newResponseBody = new MemoryStream();
                context.Response.Body = newResponseBody;

                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    await HandleBadRequestAsync(context);
                }
                else
                {
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    await newResponseBody.CopyToAsync(originalResponseBody);
                }
            }
            catch (Exception ex)
            {
                var requestPath = context.Request.Path;
                _logger.LogError(ex, "Ocorreu uma exceção não tratada no caminho {Path}.", requestPath);
                LogToFile(ex, requestPath, "Erro inesperado");
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                context.Response.Body = originalResponseBody;
            }
        }

        private async Task HandleBadRequestAsync(HttpContext context)
        {
            var requestPath = context.Request.Path;
            var resultado = Resultado<object>.ComFalha(new Erro(400, "Requisição inválida"));

            // Criando o retorno personalizado para BadRequest
            var response = new
            {
                type = "https://httpstatuses.com/400",
                title = "Bad Request",
                status = 400,
                detail = resultado.Erro.Descricao,
                traceId = context.TraceIdentifier
            };

            LogToFile(new Exception("Requisição inválida"), requestPath, "Erro de BadRequest");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var requestPath = context.Request.Path;
            var resultado = Resultado<object>.ComFalha(new Erro(500, "Erro inesperado"));

            var response = new
            {
                resultado.Erro.Codigo,
                resultado.Erro.Descricao,
                error = exception.Message,
                detalhe = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                traceId = context.TraceIdentifier
            };

            LogToFile(exception, requestPath, "Erro inesperado");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public void LogToFile(Exception ex, string requestPath, string mensagem)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Path: {requestPath} | {mensagem}: {ex.Message} {Environment.NewLine}";
            File.AppendAllText(_logFilePath, logMessage);
        }


    }
}
