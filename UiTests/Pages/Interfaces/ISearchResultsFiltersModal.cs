using static UiTests.Helpers.Enums;

namespace UiTests.Pages.Interfaces
{
    public interface ISearchResultsFiltersModal
    {
        void ClickShowChartersButton();
        void SelectFishingType(FishingType fishingType);
        void SelectReviewScore(ReviewScore score);
        void SelectTargetedSpecies(TargetedSpecies species);
        void ClickClearAllButton();
    }
}