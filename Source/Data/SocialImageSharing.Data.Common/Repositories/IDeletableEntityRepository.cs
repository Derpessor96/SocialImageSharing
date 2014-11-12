namespace SocialImageSharing.Data.Common.Repositories
{
	using System.Linq;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class
    {
        IQueryable<T> AllWithDeleted();

		IQueryable<T> AllWithDeletedIncluding(string path);

		void HardDelete(T entity);
    }
}
