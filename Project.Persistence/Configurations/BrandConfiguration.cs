using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configurations
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			Faker faker = new("tr");

			Brand brandFirst = new()
			{
				Id = 1,
				Name = faker.Commerce.Department()
			};

			Brand brandSecond = new()
			{
				Id = 2,
				Name = faker.Commerce.Department()
			};

			Brand brandThird = new()
			{
				Id = 3,
				Name = faker.Commerce.Department()
			};

			builder.HasData(brandFirst, brandSecond, brandThird);
		}
	}
}
