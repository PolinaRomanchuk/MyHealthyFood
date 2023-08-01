using Data.Interface.DataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T Add(T model);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Remove(int id);
        bool Any();
        int Count();
        
        /// <summary>
        /// DANGEROUS. Try to not use it
        /// </summary>
        void Update(T model);

        PaginatorData<T> GetPaginator(int page, int perPage);
        PaginatorData<T> GetPaginator(int page, int perPage, Func<List<T>> filteredDataFunc);
    }
}