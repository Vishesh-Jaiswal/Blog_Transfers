namespace FlightApp.Interfaces
{
    public interface IRepository<K,T>
    {
        T Add(T key);
        T Delete(K key);
        T Update(T key);
        T GetById(K key);
        IList<T> GetAll();
    }
}
