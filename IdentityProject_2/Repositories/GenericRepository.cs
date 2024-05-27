using IdentityProject_2.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject_2.Repositories
{
    public class GenericRepository<T> where T : class
    {
        public void Delete(T entity)
        {
            using var context = new Context();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public T Get(int id)
        {
            using var context = new Context();
            return context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var context = new Context();
            return context.Set<T>().ToList();
        }
        public List<T> GetList(string par)
        {
            using var context = new Context();
            return context.Set<T>().Include(par).ToList();
        }

        public void Insert(T entity)
        {
            using var context = new Context();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            using var context = new Context();
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }
    }
}
