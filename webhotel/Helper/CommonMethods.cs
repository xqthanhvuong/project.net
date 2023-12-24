using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace Webhotel.Helper
{
	public static class CommonMethods
	{
		private static string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("key");
		public static string convertToEncrypt(string code)
		{
			if (string.IsNullOrEmpty(code)) return "";
			code += key;
			var codeBytes = Encoding.UTF8.GetBytes(code);
			return Convert.ToBase64String(codeBytes);
		}
		public static string convertToDecrypt(string base64EncodeData)
		{
			if (string.IsNullOrEmpty(base64EncodeData)) return "";
			var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
			var result = Encoding.UTF8.GetString(base64EncodeBytes);
			result = result.Substring(0, result.Length - key.Length);
			return result;
		}
	}
}
