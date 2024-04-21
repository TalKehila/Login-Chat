namespace ChatServer.Modal
{
	public class MessageDto
	{
		public string Message { get; set; } = "";
		public string ToUser { get; set; } = "";
		public string FromUser { get; set; } = "";
		public string Token { get; set; } = "";
	}
}
