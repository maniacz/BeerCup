using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using BeerCup.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MainMenuItem> _menuItems;
        private readonly ISettingsService _settingsService;

        public MenuViewModel(INavigationService navigationService, ISettingsService settingsService)
            : base(navigationService)
        {
            _settingsService = settingsService;
            _menuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        public string WelcomeText => "Hello " + _settingsService.UserNameSetting;

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = (menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem;

            if (menuItem != null && menuItem.MenuText == "Wyloguj")
            {
                _settingsService.UserNameSetting = null;
                _settingsService.UserRoleSetting = Enums.UserRole.None;
                _navigationService.ClearBackStack();
            }

            var type = menuItem?.ViewModelToLoad;
            _navigationService.NavigateToAsync(type);
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Bitwa",
                ViewModelToLoad = typeof(BattleViewModel),
                MenuItemType = MenuItemType.Battle
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Wyloguj",
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Battle
            });
        }
    }
}
