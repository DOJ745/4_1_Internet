namespace LB3.Models
{
    public class CustomErrorDetails
    {
		public int code;
		public string message;
		public CustomErrorDetails(int code, string message)
		{
			this.code = code;
			this.message = message;
		}
	}
}