using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;
using ValuationApi.Services.Repository;

namespace ValuationApi.Services.ValuationServices
{   
    public class ValuationService
    {
        private Repository<Valuation> repository;        

        public ValuationService(ApiContext context)
        {
            this.repository = new Repository<Valuation>(context);            
        }

        public IEnumerable<Valuation> Get()
        {
            IEnumerable<Valuation> list = repository.GetAll();
            return list;
        }

        public Valuation GetById(int id)
        {
            Valuation valuation = repository.GetById(id);
            return valuation;
        }

        public IEnumerable<Valuation> GetByImoNumber(int id)
        {
            IEnumerable<Valuation> list = repository.GetAll().Where(v => v.ImoNumber == id);
            return list;
        }

        public void Create(Valuation valuation)
        {
            repository.Create(valuation);
        }

        public void Update(Valuation valuation)
        {
            repository.Update(valuation);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
