namespace AppHttpExceptionHandling.Models
{
	public class ApiErrorModel
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }
	}
}