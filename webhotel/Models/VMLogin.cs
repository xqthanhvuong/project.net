namespace Webhotel.Models
{
	public class VMLogin
	{
		public string Username { get; set; } = null!;

		public string Password { get; set; }

		public bool KeepLoggedIn { get; set; }

	}
}
