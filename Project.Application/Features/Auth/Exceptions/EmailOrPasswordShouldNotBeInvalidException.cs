using Project.Application.Bases;

namespace Project.Application.Features.Auth.Exceptions
{
	public class EmailOrPasswordShouldNotBeInvalidException : BaseExceptions
	{
		public EmailOrPasswordShouldNotBeInvalidException() : base("Kullanıcı adı veya şifre hatalı!")
		{

		}
	}
}
