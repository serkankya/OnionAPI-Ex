using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces.CustomAutoMapper;

namespace Project.Mapper.CustomAutoMapper
{
	public class CustomMapper : ICustomMapper
	{
		public static List<TypePair> typePairs = new();
		private IMapper MapperContainer;
		private readonly ILoggerFactory _loggerFactory;

		public CustomMapper(ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory;
		}

		protected void Config<TDestionation, TSource>(int depth = 5, string? ignore = null)
		{
			var typePair = new TypePair(typeof(TSource), typeof(TDestionation));

			if (typePairs.Any(a => a.DestinationType == typePair.DestinationType && a.SourceType == typePair.SourceType) && ignore is null)
				return;

			typePairs.Add(typePair);

			var config = new MapperConfiguration(cfg =>
			{
				foreach (var item in typePairs)
				{
					if (ignore is not null)
						cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore, x => x.Ignore()).ReverseMap();
					else
						cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
				}
			},_loggerFactory);

			MapperContainer = config.CreateMapper();
		}

		public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);
			return MapperContainer.Map<TDestination>(source);
		}

		public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);
			return MapperContainer.Map<IList<TDestination>>(source);
		}

		public TDestination Map<TDestination>(object source, string? ignore = null)
		{
			Config<TDestination, object>(5, ignore);
			return MapperContainer.Map<TDestination>(source);
		}

		public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
		{
			Config<TDestination, IList<object>>(5, ignore);
			return MapperContainer.Map<IList<TDestination>>(source);
		}
	}
}
