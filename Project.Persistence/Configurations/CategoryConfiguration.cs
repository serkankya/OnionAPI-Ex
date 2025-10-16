using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			Category categoryFirst = new()
			{
				Id = 1,
				Name = "Elektronik",
				Priority = 1,
				ParentId = 0,
				IsDeleted = false,
				CreatedAt = DateTime.Now
			};

			Category categorySecond = new()
			{
				Id = 2,
				Name = "Moda",
				Priority = 2,
				ParentId = 0,
				IsDeleted = false,
				CreatedAt = DateTime.Now
			};

			Category parentFirst = new()
			{
				Id = 3,
				Name = "Bilgisayar",
				Priority = 1,
				ParentId = 1,
				IsDeleted = false,
				CreatedAt = DateTime.Now
			};

			Category parentSecond = new()
			{
				Id = 4,
				Name = "Kadın",
				Priority = 1,
				ParentId = 2,
				IsDeleted = false,
				CreatedAt = DateTime.Now
			};

			builder.HasData(categoryFirst, categorySecond, parentFirst, parentSecond);
		}
	}
}
