using FlightApp.Models;

namespace FlightApp.Interfaces
{
    public interface IFlightService
    {
        public IList<Flight> GetAll();
        public Flight GetById(int id);
        public Flight AddFlight(Flight flight);
        public Flight UpdateFlight(Flight flight);
        public Flight DeleteFlight(int id);
    }
}
