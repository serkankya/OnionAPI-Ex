namespace Project.Domain.Common
{
	public class EntityBase : IEntityBase
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public bool IsDeleted { get; set; } = false;
	}
}
