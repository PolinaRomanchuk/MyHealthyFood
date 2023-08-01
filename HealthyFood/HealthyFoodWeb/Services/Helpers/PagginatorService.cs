using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Sql.Repositories;
using HealthyFoodWeb.Models;

namespace HealthyFoodWeb.Services.Helpers
{
    public class PagginatorService : IPagginatorService
    {
        public PagginatorViewModel<TViewModel>
            GetPaginatorViewModel<TViewModel, TDbModel>(
                int page,
                int perPage,
                Func<TDbModel, TViewModel> buildViewModelFunc,
                IBaseRepository<TDbModel> repository)
            where TViewModel : class
            where TDbModel : BaseModel
        {
            var dbPagginator = repository
                .GetPaginator(page, perPage);

            var viewModel = new PagginatorViewModel<TViewModel>();
            viewModel.Items = dbPagginator
                .Items
                .Select(x=> buildViewModelFunc(x))
                .ToList();

            var doWeNeedOneMorePage = dbPagginator.TotalCount % perPage != 0;
            var totalPageCount =
                (dbPagginator.TotalCount / perPage)
                + (doWeNeedOneMorePage ? 1 : 0);

            viewModel.PageList = Enumerable
                .Range(1, totalPageCount)
                .ToList();
            viewModel.ActivePageNumber = page;

            return viewModel;
        }

        public PagginatorViewModel<TViewModel>
            GetPaginatorViewModel<TViewModel, TDbModel>(
                int page,
                int perPage,
                Func<List<TDbModel>> filteredDataFunc,
                Func<TDbModel, TViewModel> buildViewModelFunc,
                IBaseRepository<TDbModel> repository)
            where TViewModel : class
            where TDbModel : BaseModel
        {
            var dbPagginator = repository
                .GetPaginator(page, perPage, filteredDataFunc);

            var viewModel = new PagginatorViewModel<TViewModel>();
             viewModel.Items = dbPagginator
                .Items
                .Select(x=> buildViewModelFunc(x))
                .ToList();

            var doWeNeedOneMorePage = dbPagginator.TotalCount % perPage != 0;
            var totalPageCount =
                (dbPagginator.TotalCount / perPage)
                + (doWeNeedOneMorePage ? 1 : 0);

            viewModel.PageList = Enumerable
                .Range(1, totalPageCount)
                .ToList();
            viewModel.ActivePageNumber = page;

            return viewModel;
        }
    }
}
