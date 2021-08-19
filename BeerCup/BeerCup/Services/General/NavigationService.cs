using BeerCup.Bootstrap;
using BeerCup.Contracts.Services.General;
using BeerCup.ViewModels;
using BeerCup.ViewModels.Base;
using BeerCup.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeerCup.Services.General
{
    public class NavigationService : INavigationService
    {
        Dictionary<Type, Type> _mappings;

        private Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
        }

        public async Task InitializeAsync()
        {
            //todo: authentication
            await NavigateToAsync<MainViewModel>();
        }

        private Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType);

            CurrentApplication.MainPage = page;

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
    }
}
