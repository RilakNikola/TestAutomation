using UiTests.Helpers;
using UiTests.Managers;
using UiTests.Managers.Interfaces;
using UiTests.Pages;
using UiTests.Pages.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestFramework.Config;
using TestFramework.Driver;
using TestFramework.Helpers;

namespace UiTests
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigReader.ReadConfig());
            services.AddScoped<IDriverFixture, DriverFixture>();
            services.AddScoped<IDriverWait, DriverWait>();
            services.AddScoped<IDriverUtilities, DriverUtilities>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IHomePage, HomePage>();
            services.AddScoped<ILoginModal, LoginModal>();
            services.AddScoped<ISearchResultsFiltersModal, SearchResultsFiltersModal>();
            services.AddScoped<ISearchResultsPage, SearchResultsPage>();
            services.AddScoped<ICharterViewPage, CharterViewPage>();
            services.AddScoped<ISendMessageModal, SendMessageModal>();
            services.AddScoped<IMessageSentModal, MessageSentModal>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<ISearchManager, SearchManager>();
            services.AddScoped<ICharterViewPage, CharterViewPage>();
            services.AddScoped<ICharterViewManager, CharterViewManager>();
        }
    }
}
