using System.Net;

namespace AppHttpExceptionHandling.Exceptions
{
	public class AppHttpException : Exception
	{
		public HttpStatusCode StatusCode { get; }
		
		public AppHttpException(string msg, HttpStatusCode status = HttpStatusCode.InternalServerError) : base(msg)
		{
			StatusCode = status;
		}
		
	}
}