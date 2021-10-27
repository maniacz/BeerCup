using BeerCup.Mobile.ViewModels;
using BeerCup.Mobile.ViewModels.Base;
using BeerCup.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Bootstrap;
using BeerCup.Mobile.Contracts.Services.Data;

namespace BeerCup.Mobile.Services.General
{
    public class NavigationService : INavigationService
    {
        Dictionary<Type, Type> _mappings;
        private readonly IAuthenticationService _authenticationService;

        private Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
            _authenticationService = authenticationService;
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
            _mappings.Add(typeof(MenuViewModel), typeof(MenuView));
            _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(BattleViewModel), typeof(BattleView));
            _mappings.Add(typeof(BreweryStatsViewModel), typeof(BreweryStatsView));
            _mappings.Add(typeof(RegisterViewModel), typeof(RegisterView));
            _mappings.Add(typeof(AdminPanelViewModel), typeof(AdminPanelView));
            _mappings.Add(typeof(ResultsViewModel), typeof(ResultsView));
            _mappings.Add(typeof(ResultsCatalogViewModel), typeof(ResultsCatalogView));
        }

        public async Task InitializeAsync()
        {
            if (_authenticationService.IsUserAuthenticated())
            {
                await NavigateToAsync<MainViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType);

            if (page is MainView || page is RegisterView || page is LoginView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;

                if (mainPage.Detail is BeerCupNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new BeerCupNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as BeerCupNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new BeerCupNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for {viewModelType} found in navigation mappings");
            }

            return _mappings[viewModelType];
        }

        public async Task ClearBackStack()
        {
            await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PopToRootAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                await mainPage.Detail.Navigation.PopToRootAsync();
            }
        }

        //todo: zaimplementuj NavigateBackAsync() i PopToRootAsync() jeśli będzie potrzebne
    }
}
