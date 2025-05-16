using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext context)
    {
       
        var logPath = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\ExceptionLog.txt";
        var logContent = $"[{DateTime.Now}] {context.Exception.Message}\n{context.Exception.StackTrace}\n--------------------------------------------------------------------";
        System.IO.File.AppendAllText(logPath, logContent);

        var errorResponse = new
        {
            Message = "Something went wrong. Please contact support.",
            Details = context.Exception.Message 
        };

        context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
    }
}
