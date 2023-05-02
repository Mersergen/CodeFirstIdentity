using MVCWithDB_Identity.Models;

namespace MVCWithDB_Identity.Services
{

	public class BaseService<T> where T : class
	{
		public North DB;
		public BaseService()
		{
			DB = new North();
		}
		public void AddEntity(T entity)
		{
			DB.Set<T>().Add(entity);
			DB.SaveChanges();
		}

		public T? GetEntity(int id)
		{
			var entity = DB.Set<T>().Find(id);

			return entity;
		}

		public IQueryable<T> List()
		{
			return DB.Set<T>();
		}

		public void RemoveEntity(int? id)
		{
			var entity = DB.Set<T>().Find(id);

			if (entity == null) return;

			DB.Set<T>().Remove(entity);
			DB.SaveChanges();

		}

		public void UpdateEntity(T entity)
		{
			if (entity == null) return;

			DB.Set<T>().Update(entity);
			DB.SaveChanges();
		}
	}
}
