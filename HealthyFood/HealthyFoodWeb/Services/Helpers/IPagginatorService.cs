using Data.Interface.Models;
using Data.Interface.Repositories;
using HealthyFoodWeb.Models;

namespace HealthyFoodWeb.Services.Helpers
{
    public interface IPagginatorService
    {
        PagginatorViewModel<TViewModel> GetPaginatorViewModel<TViewModel, DbModel>(int page, int perPage, Func<DbModel, TViewModel> buildViewModelFunc, IBaseRepository<DbModel> repository)
            where TViewModel : class
            where DbModel : BaseModel;
        PagginatorViewModel<TViewModel> GetPaginatorViewModel<TViewModel, DbModel>(int page, int perPage, Func<List<DbModel>> filteredDataFunc,  Func<DbModel, TViewModel> buildViewModelFunc, IBaseRepository<DbModel> repository)
           where TViewModel : class
           where DbModel : BaseModel;
    }
}