using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = BSI.Integra.Aplicacion.Base.Exceptions.KeyNotFoundException;
using NotImplementedException = BSI.Integra.Aplicacion.Base.Exceptions.NotImplementedException;
using UnauthorizedAccessRequestException = BSI.Integra.Aplicacion.Base.Exceptions.UnauthorizedAccessRequestException;

namespace BSI.Integra.Servicios.Configurations
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IValorEstatico valorEstatico)
        {
            try
            {
                if (!ValorEstatico.CargaInicial)
                {
                    valorEstatico.CargarConfiguracion();
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            string stackTrace = string.Empty;
            string codigoError = string.Empty;
            string message = FormatStackTrace(ex.StackTrace);
            stackTrace = message;
            //stackTrace = ex.StackTrace;
            var exceptionType = ex.GetType();

            //message = ex.Message;
            message = FormatMessage(ex.Message, ref codigoError);
            if (exceptionType == typeof(BadRequestException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessRequestException))
            {
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(ConflictException))
            {
                status = HttpStatusCode.Conflict;
            }
            else
            {
                status = HttpStatusCode.BadRequest;
            }
            var exceptionResult = JsonSerializer.Serialize(new { error = message, codigoError, status, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
        private static string FormatMessage(string message, ref string codigoError)
        {
            if (message.StartsWith("#"))
            {
                codigoError = message.Split("@")[0];
                return message.Split("@")[1];
            }
            else
            {
                codigoError = "No Generado";
                return message;
            }
        }
        private static string FormatStackTrace(string stackTrace)
        {
            var res1 = stackTrace.Split("(");
            if (res1.Count() > 0)
            {
                var res2 = res1[0].Split(".");
                if (res2.Count() > 0)
                {
                    var funcion = res2.LastOrDefault();
                    var linea = stackTrace.Split("line")[1].Replace("\r\n", "<>").Split("<>").FirstOrDefault();
                    if (res2.Count() > 2)
                    {
                        var archivo = res2.ElementAt(res2.Count() - 2);
                        return $"Error Generado En {archivo}; {funcion}(); Linea: {linea.Trim()}";
                    }
                    else
                        return $"Error Generado En {funcion}(); Linea: {linea}";
                }
                else
                    return stackTrace;
            }
            else
            {
                return stackTrace;
            }
        }
        static string GetCallingMethodName()
        {
            var stackTrace = new StackTrace();
            var callingFrame = stackTrace.GetFrame(2);
            return callingFrame.GetMethod().Name;
        }
        static int GetCallingLineNumber()
        {
            var stackTrace = new StackTrace();
            var callingFrame = stackTrace.GetFrame(2); // El índice 2 representa el llamador del llamador
            var ss = callingFrame.GetFileName();
            return callingFrame.GetFileLineNumber();
        }

        static int GetExceptionLineNumber(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrame(0);
            return frame.GetFileLineNumber();
        }
    }
}
