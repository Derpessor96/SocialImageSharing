namespace SocialImageSharing.Data.Common.Repositories
{
	using System.Data.Entity;
	using System.Linq;

	using SocialImageSharing.Data.Common.Models;
	using System;

	public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>
		where T : class, IDeletableEntity
	{
		public DeletableEntityRepository(DbContext context)
			: base(context)
		{
		}

		public override IQueryable<T> All()
		{
			return base.All().Where(x => !x.IsDeleted);
		}

		public override IQueryable<T> AllIncluding(string path)
		{
			return base.AllIncluding(path).Where(x => !x.IsDeleted);
		}

		public IQueryable<T> AllWithDeleted()
		{
			return base.All();
		}

		public IQueryable<T> AllWithDeletedIncluding(string path)
		{
			return base.AllIncluding(path).Where(x => !x.IsDeleted);
		}

		public override void Delete(T entity)
		{
			entity.IsDeleted = true;
			entity.DeletedOn = DateTime.Now;
			base.ChangeEntityState(entity, EntityState.Modified);
		}

		public void HardDelete(T entity)
		{
			base.Delete(entity);
		}
	}
}
