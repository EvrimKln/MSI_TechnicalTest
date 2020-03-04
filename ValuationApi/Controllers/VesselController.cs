using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValuationApi.Model;
using ValuationApi.Services.Repository;
using ValuationApi.Services.VesselServices;

namespace ValuationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VesselController : ControllerBase
    {
        private VesselService vesselService;
        private readonly ApiContext _context;

        public VesselController(ApiContext context)
        {
            _context = context;
            this.vesselService = new VesselService(context);

        }      

        // GET: api/Vessel        
        [HttpGet]        
        public IEnumerable<Vessel> Get()
        {
            IEnumerable<Vessel> list = vesselService.Get();
            return list;
        }

        // GET: api/Vessel/5        
        [HttpGet("{id}")]        
        public Vessel Get(int id)
        {
            Vessel vessel = vesselService.GetById(id);
            return vessel;
        }

        // GET: api/Vessel/GetByImoNumber/5
        [HttpGet("GetByImoNumber/{imoNumber}")]
        //or below
        //[HttpGet]
        //[Route("GetByImoNumber/{imoNumber}")]
        public Vessel GetByImoNumber(int imoNumber)
        {
            Vessel vessel = vesselService.GetByImoNumber(imoNumber);
            return vessel;
        }

        // POST: api/vessel
        [HttpPost]
        public void Post(Vessel vessel)
        {
            vesselService.Create(vessel);
        }

        // POST: api/vessel/update
        [HttpPost]
        public void Put(Vessel vessel)
        {
            vesselService.Update(vessel);
        }

        // DELETE: api/vessel/delete/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            vesselService.Delete(id);
        }
    }
}
