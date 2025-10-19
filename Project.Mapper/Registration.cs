using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Mapper.CustomAutoMapper;

namespace Project.Mapper
{
	public static class Registration
	{
		public static void AddCustomMapper(this IServiceCollection services)
		{
			services.AddSingleton<ICustomMapper, CustomMapper>();
		} 
	}
}
