using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configurations
{
	public class DetailConfiguration : IEntityTypeConfiguration<Detail>
	{
		public void Configure(EntityTypeBuilder<Detail> builder)
		{
			Faker faker = new("tr");

			Detail detailFirst = new()
			{
				Id = 1,
				Title = faker.Lorem.Sentence(2),
				Description = faker.Lorem.Sentence(6),
				CategoryId = 1,
				CreatedAt = DateTime.Now,
				IsDeleted = false
			};

			Detail detailSecond = new()
			{
				Id = 2,
				Title = faker.Lorem.Sentence(1),
				Description = faker.Lorem.Sentence(6),
				CategoryId = 3,
				CreatedAt = DateTime.Now,
				IsDeleted = true
			};

			Detail detailThird = new()
			{
				Id = 3,
				Title = faker.Lorem.Sentence(1),
				Description = faker.Lorem.Sentence(5),
				CategoryId = 4,
				CreatedAt = DateTime.Now,
				IsDeleted = false
			};

			builder.HasData(detailFirst, detailSecond, detailThird);
		}
	}
}
