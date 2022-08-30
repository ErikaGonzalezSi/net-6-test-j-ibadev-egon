/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/

using System.Net;
using net_6_test_j_ibadev_egon_pr.Models;
using System.Text.Json;

namespace net_6_test_j_ibadev_egon_pr.Controllers;

public class ErrorHand
{
    private readonly RequestDelegate _next;

    public ErrorHand(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    // Handle technical error and convert in format standard
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var codeerror = HttpStatusCode.InternalServerError;
        ErrorMessage error = new ErrorMessage();
        error.Error = "Error";//Title
        error.ErrorDescription = "Error contact your administrator";
        error.TechnicalError = exception?.InnerException?.Message;//Error details technical error message
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)codeerror;
        return context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}