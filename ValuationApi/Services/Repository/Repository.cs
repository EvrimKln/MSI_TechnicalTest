using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private ApiContext _context = null;
        private DbSet<T> table = null;
   
        public Repository(ApiContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            //Our purpose just reading so AsNoTracking increase performans
            //I just assume we read all active records everytime, it depands on the needs
            return table.Where(t => t.IsActive).AsNoTracking();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Create(T obj)
        {
            obj.CreationDate = DateTime.Today;
            obj.CreatedBy = 1;
            obj.IsActive = true;
            table.Add(obj);

            Save();
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            obj.UpdateDate = DateTime.Today;
            obj.UpdatedBy = 1;
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }
        public void Delete(object id)
        {           
            T existing = table.Find(id);
            existing.IsActive = false;
            existing.UpdateDate = DateTime.Today;
            existing.UpdatedBy = 1;
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
