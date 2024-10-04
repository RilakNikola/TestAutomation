using UiTests.Helpers;

namespace UiTests.Managers.Interfaces
{
    public interface ISearchManager
    {
        void SelectFilters(FiltersData filtersData);
        void SelectResult(ResultsData resultsData);
        void FillAndSubmitSearchForm(SearchFormData searchForm);
    }
}