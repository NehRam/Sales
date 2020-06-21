namespace Sales.Helpers
{
	using System;
	using System.Net.Mail;

	public class RegexHelper
    {
        public static bool IsValidEmailAddress(string emailadress)
        {
			try
			{
				var email = new MailAddress(emailadress);
				return true;
			}
			catch (FormatException)
			{

				return false;
			}
        }
    }
}
