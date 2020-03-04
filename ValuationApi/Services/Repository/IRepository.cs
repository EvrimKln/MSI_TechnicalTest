using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Create(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
