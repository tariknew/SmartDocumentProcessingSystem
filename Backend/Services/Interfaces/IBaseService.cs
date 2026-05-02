namespace Services.Interfaces
{
    public interface IBaseService<TEntity, TSearch, TInsert, TUpdate> where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        IEnumerable<TEntity> Get(TSearch? searchObject = null);
        IEnumerable<TEntity> Delete(int id);
        TEntity GetById(int id);
        TEntity Insert(TInsert request);
        TEntity Update(int id, TUpdate request);
    }
}
