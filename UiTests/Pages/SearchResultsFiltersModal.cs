using UiTests.Helpers;
using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;
using static UiTests.Helpers.Enums;

namespace UiTests.Pages
{
    public class SearchResultsFiltersModal(IDriverWait driver, IActionsService actionsService) : ISearchResultsFiltersModal
    {

        #region Element locators
        private IWebElement ShowChartersButton => driver.FindElement(By.XPath("//button[contains(text(),'Show')]"));
        private IWebElement ClearAll => driver.FindElement(By.XPath("//a[text()='Clear all']"));
        private static By Show => By.XPath("//button[contains(text(),'Show')]");

        private IWebElement GetReviewScoreElement(ReviewScore score)
        {
            string scoreStr = score switch
            {
                ReviewScore.FourTwentyFive => "4_25",
                ReviewScore.FourFifty => "4_50",
                ReviewScore.FourSeventyFive => "4_75",
                _ => throw new ArgumentOutOfRangeException(nameof(score), $"Unsupported review score: {score}")
            };

            return driver.FindElement(By.CssSelector($"input[name='{scoreStr}']"), DriverWait.WaitCondition.Exists);
        }

        private IWebElement GetFishingTypeElement(FishingType fishingType)
        {
            string fishingTypeStr = fishingType switch
            {
                FishingType.InshoreFishing => "fishing_type_inshore",
                FishingType.NearshoreFishing => "fishing_type_nearshore",
                FishingType.OffshoreFishing => "fishing_type_offshore",
                FishingType.RiverFishing => "fishing_type_river",
                FishingType.LakeFishing => "fishing_type_lake",
                FishingType.ReefFishing => "fishing_type_reef",
                FishingType.WreckFishing => "fishing_type_wreck",
                FishingType.FlatsFishing => "fishing_type_flats",
                FishingType.BackcountryFishing => "fishing_type_backcountry",
                _ => throw new ArgumentOutOfRangeException(nameof(fishingType), $"Unsupported fishing type: {fishingType}")
            };

            return driver.FindElement(By.CssSelector($"input[name='{fishingTypeStr}']"), DriverWait.WaitCondition.Exists);
        }

        private IWebElement GetTargetedSpeciesElement(TargetedSpecies species)
        {
            string speciesStr = species switch
            {
                TargetedSpecies.KingMackerelKingfish => "king_mackerel_kingfish",
                TargetedSpecies.DolphinMahiMahi => "dolphin_mahi_mahi",
                TargetedSpecies.SnapperMangrove => "snapper_mangrove",
                TargetedSpecies.Cobia => "cobia",
                TargetedSpecies.Amberjack => "amberjack",
                TargetedSpecies.GrouperGag => "grouper_gag",
                TargetedSpecies.SnapperRed => "snapper_red",
                TargetedSpecies.SnapperPink => "snapper_pink",
                TargetedSpecies.TigerFish => "tiger_fish",
                TargetedSpecies.YellowFish => "yellow_fish",
                _ => throw new ArgumentOutOfRangeException(nameof(species), $"Unsupported species: {species}")
            };

            return driver.FindElement(By.CssSelector($"input[name='{speciesStr}']"), DriverWait.WaitCondition.Exists);
        }
        #endregion

        #region Public methods
        public void SelectReviewScore(ReviewScore score) => actionsService.ClickElement(GetReviewScoreElement(score));
        public void SelectFishingType(FishingType fishingType) => actionsService.ClickElement(GetFishingTypeElement(fishingType));
        public void SelectTargetedSpecies(TargetedSpecies species) => actionsService.ClickElement(GetTargetedSpeciesElement(species));
        public void ClickShowChartersButton() => ShowChartersButton.Click();
        public void ClickClearAllButton()
        {
            driver.FindElement(Show, DriverWait.WaitCondition.Visible);
            ClearAll.Click();
        }
        #endregion
    }
}
