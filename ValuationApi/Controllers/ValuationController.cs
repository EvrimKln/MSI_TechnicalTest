using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValuationApi.Model;
using Microsoft.EntityFrameworkCore;
using ValuationApi.Services.ValuationServices;
using ValuationApi.Services.VesselServices;

namespace ValuationApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuationController : ControllerBase
    {
        private VesselService vesselService;
        private ValuationService valuationService;       
        private readonly ApiContext _context;
        private readonly IValuator _valuator;

        public ValuationController(ApiContext context, IValuator valuator)
        {
            _valuator = valuator;
            _context = context;
            vesselService = new VesselService(context);
            valuationService = new ValuationService(context);            
        }

        [HttpPost]
        [Route("valuate")]
        public IEnumerable<Valuation> ValuateVessels(List<int> vesselImoNumbers)
        {
            try
            {
                //vesselImoNumbers --> Imo Numbers for the selected vessels 
                //I assume that all selected vessels are active(Beacuse we show active vessells to user) and built before 2020
                foreach (var vesselImoNumber in vesselImoNumbers)
                {
                    Vessel vessel = vesselService.GetByImoNumber(vesselImoNumber);

                    //////Valuate if the vessel has no valuation
                    //////Valuate again if the vessel has valuation but is valuated in previous years. 
                    //  //Since the age of vessel changed,it is more healthy to valuate again.

                    //////Valuate again if the vessel has valuation but one of the property of the vessel(like size) is changed and 
                    //  //it affects the valuation. I assume that vessel model just contains informations for valuation, 
                    //  //so that i just checked the update date to valuate again or not.
                    //  //But normally vessel model probably contains a lot of properties apart from these. 
                    //  //So i would design a log to follow the changes about necessary properties and check this log to decide.
                    //  //Or i would define a few trigger for changes and this trigger invoke a job which do valuation, 
                    //  //so everytime we would have up to date valutaion. Depands on needs.

                    List<Valuation> existingValuations = _context.Valuations.Where(v => v.IsActive && v.ImoNumber == vessel.ImoNumber && v.VesselTypeId == vessel.VesselTypeId).ToList();
                    
                    //Normally we can control with this statement --> vessel.Valuation.Any() in relational database
                    if ((existingValuations == null || !existingValuations.Any()) || (existingValuations.FirstOrDefault().CreationDate.Year != DateTime.Today.Year)
                        || (existingValuations.FirstOrDefault().CreationDate > vessel.UpdateDate))
                    {                       
                        //// valuation changes depond on vessel type, i avoid to do if block which valuate according to vessel type
                        //// i have classses inherited base abstract class for each vessel type
                        //// Each class responsible its valuation, formule
      
                        IEnumerable<TimeSeries> timeSeries = _context.TimeSeries.Where(t => t.VesselTypeId == vessel.VesselTypeId);
                        List<Valuation> allValuations = _valuator.Valuate(vessel, timeSeries);
                        foreach (var item in allValuations)
                        {
                            valuationService.Create(item);
                        }
                    }
                };
                Vessel vesseldd = vesselService.GetByImoNumber(1);

                // After we valuate for the vessels which need to be valuated, we can get vessels and its valuations
                // isActive control is added in case the vessel may be deleted by other users meanwhile the process
                return valuationService.Get().Where(v => vesselImoNumbers.Contains(v.ImoNumber)).OrderBy(v => v.ImoNumber).ThenBy(v => v.Year);
                
                
                //var vals = _context.Valuations
                //           .Where(v => vesselImoNumbers.Contains(v.ImoNumber))
                //           .OrderBy(v => v.ImoNumber)
                //           .ThenBy(v => v.Year)                         
                //           .Select(v => new { v.Id, v.Year, v.FairMarketValue, v.OperatingCosts, v.ScrapPrice, v.Age });               
            }
            catch (DivideByZeroException exZero)
            {
                throw exZero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Valuation/5
        [HttpGet("{id}", Name = "Get")]
        [Route("{id:int}")]
        public IEnumerable<Valuation> Get(int id)
        {
            IEnumerable<Valuation> list = _context.Valuations.Where(v => v.ImoNumber == id).OrderBy(v => v.Year);

            return list;
        }

        // GET: Valuation
        [HttpGet]
        public IEnumerable<Valuation> Get()
        {
            var list = _context.Valuations.Where(v => v.Vessel.IsActive).OrderBy(v => v.ImoNumber).ThenBy(v => v.Year);
            return list;
        }
    }
}
