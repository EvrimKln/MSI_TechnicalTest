using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Services.Repository;
using ValuationApi.Model;

namespace ValuationApi.Services.VesselServices
{
    public class VesselService
    {
        private Repository<Vessel> repository;
        public VesselService(ApiContext context)
        {
            this.repository = new Repository<Vessel>(context);
        }

        public IEnumerable<Vessel> Get()
        {
            IEnumerable<Vessel> list = repository.GetAll();
            return list;
        }

        public Vessel GetById(int id)
        {
            Vessel vessel = repository.GetById(id);
            return vessel;
        }
       
        public Vessel GetByImoNumber(int imoNumber)
        {
            Vessel vessel = repository.GetAll().FirstOrDefault(v => v.ImoNumber == imoNumber);
            return vessel;
        }
        
        public void Create(Vessel vessel)
        {
            repository.Create(vessel);
        }
       
        public void Update(Vessel vessel)
        {
            repository.Update(vessel);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}