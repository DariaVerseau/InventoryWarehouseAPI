namespace DAL.Interfaces;

public interface IRepository<T, in TC, in TU>
{
    Task<List<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Create(TC book);
    Task<T> Update(TU book);
    Task Delete(Guid id);
}