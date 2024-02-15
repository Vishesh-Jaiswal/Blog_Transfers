using FlightApp.Exceptions;
using FlightApp.Interfaces;
using FlightApp.Models;

namespace FlightApp.Services
{
    public class FlightService:IFlightService
    {
        private readonly IRepository<int, Flight> _repository;

        public FlightService(IRepository<int, Flight> repository)
        {
            _repository = repository;
        }

        public Flight AddFlight(Flight flight)
        {
            var result = _repository.Add(flight);
            return result;
        }

        public Flight DeleteFlight(int id)
        {
            var result = _repository.Delete(id);
            return result;
        }

        public IList<Flight> GetAll()
        {
            var result=_repository.GetAll();
            if (result == null)
                return null;
            return result;
        }

        public Flight GetById(int id)
        {
            var result= _repository.GetById(id);
            if (result == null)
            {
                throw new CouldNotFetch();
            }
            return result;
        }

        public Flight UpdateFlight(Flight flight)
        {
            var result= _repository.Update(flight);
            if (result == null)
            {
                throw new CouldNotEdit();
            }
            return result;
        }
    }
}
