using FlightApp.Contexts;
using FlightApp.Interfaces;
using FlightApp.Models;

namespace FlightApp.Repositories
{
    public class FlightRepository:IRepository<int, Flight>
    {
        private readonly FlightAppDBContext _dbContext;

        public FlightRepository(FlightAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Flight Add(Flight key)
        {
            _dbContext.Flights.Add(key);
            _dbContext.SaveChanges();
            return key;
        }

        public Flight Delete(int key)
        {
            var result = GetById(key);
            if (result != null)
            {
                _dbContext.Flights.Remove(result);
                _dbContext.SaveChanges();
                return result;
            }
            return null;
        }

        public IList<Flight> GetAll()
        {
            if (_dbContext.Flights.Count() == 0)
                return null;
            return _dbContext.Flights.ToList();
        }

        public Flight GetById(int key)
        {
            var resut = _dbContext.Flights.FirstOrDefault(f=>f.FlightId==key);
            if(resut != null)
            {
                return resut;
            }
            return null;
        }

        public Flight Update(Flight key)
        {
            var result = GetById(key.FlightId);
            if (result != null)
            {
                _dbContext.Entry(result).CurrentValues.SetValues(key);
                _dbContext.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
