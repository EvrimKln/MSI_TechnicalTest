using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.ValuationServices
{
    public class DefaultValuator : IValuator
    {        
        private readonly List<IValuation> _valuations;

        public DefaultValuator()
        {
            //in the future, if we have another valuation for a new vessel type, 
            //we just need to create a new class for valuation and add here
            //existing classes or valuations will not be affected
            _valuations = new List<IValuation>();
            _valuations.Add(new DryBulkValuation());
            _valuations.Add(new OilTankerValuation());
            _valuations.Add(new ContainershipValuation());
        }

        public List<Valuation> Valuate(Vessel vessel, IEnumerable<TimeSeries> timeSeries)
        {
            var vVesselType = _valuations.First(r => r.IsMatch(vessel));
            return vVesselType.Valuate(vessel, timeSeries);
        }
    }
}
