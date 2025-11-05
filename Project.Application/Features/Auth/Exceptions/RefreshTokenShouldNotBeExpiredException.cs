using Project.Application.Bases;

namespace Project.Application.Features.Auth.Exceptions
{
	public class RefreshTokenShouldNotBeExpiredException : BaseExceptions
	{
		public RefreshTokenShouldNotBeExpiredException() : base("Lütfen yeniden giriş yapınız.")
		{
			
		}
	}
}
