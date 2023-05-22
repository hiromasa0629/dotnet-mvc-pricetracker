using AppHttpExceptionHandling.Models;
using AppHttpExceptionHandling.Exceptions;
using Newtonsoft.Json;
using System.Net;


namespace AppHttpExceptionHandling.Middleware
{
	public class AppHttpExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppHttpException exception)
            {
                string errorId = Guid.NewGuid().ToString();
				
                var errorResult = new ApiErrorModel
                {
					Message = exception.Message,
					StatusCode = ((int)exception.StatusCode),
                };

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
            }
			catch (Exception exception)
			{
				var errorResult = new ApiErrorModel
				{
					Message = exception.Message,
					StatusCode = ((int)HttpStatusCode.BadRequest)
				};
				
				var response = context.Response;
				if (!response.HasStarted)
				{
					response.ContentType = "application/json";
					response.StatusCode = errorResult.StatusCode;
					await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
				}
			}
		}
	}
}