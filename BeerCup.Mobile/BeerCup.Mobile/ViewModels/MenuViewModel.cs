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
        private ObservableCollection<MainMenuItem> _userMenuItems;
        private readonly ISettingsService _settingsService;
        private Dictionary<MenuItemType, MainMenuItem> _menuItems;

        public MenuViewModel(INavigationService navigationService, ISettingsService settingsService)
            : base(navigationService)
        {
            _settingsService = settingsService;
            _userMenuItems = new ObservableCollection<MainMenuItem>();
            _menuItems = PopulateMenuItems();

            switch (_settingsService.UserRoleSetting)
            {
                case UserRole.None:
                    //LoadDefaultMenuItems();
                    break;
                case UserRole.Admin:
                    LoadAdminMenuItems();
                    break;
                case UserRole.BreweryOwner:
                    LoadBreweryStatsMenuItems();
                    break;
                case UserRole.Voter:
                    LoadVoterMenuItems();
                    break;
                default:
                    break;
            }
            //LoadMenuItems();
        }

        private Dictionary<MenuItemType, MainMenuItem> PopulateMenuItems()
        {
            var menuItems = new Dictionary<MenuItemType, MainMenuItem>();

            menuItems.Add(MenuItemType.Battle, new MainMenuItem
            {
                MenuText = "Bitwa",
                ViewModelToLoad = typeof(BattleViewModel),
                MenuItemType = MenuItemType.Battle
            });
            menuItems.Add(MenuItemType.Logout, new MainMenuItem
            {
                MenuText = "Wyloguj",
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout
            });
            menuItems.Add(MenuItemType.VoterHistory, new MainMenuItem
            {
                MenuText = "Bitwy na których byłem",
                ViewModelToLoad = typeof(VoterHistoryViewModel),
                MenuItemType = MenuItemType.VoterHistory
            });
            menuItems.Add(MenuItemType.AdminPanel, new MainMenuItem
            {
                MenuText = "Panel Admina",
                ViewModelToLoad = typeof(VoterHistoryViewModel),
                MenuItemType = MenuItemType.AdminPanel
            });
            menuItems.Add(MenuItemType.BreweryStats, new MainMenuItem
            {
                MenuText = "Statystyki mojego browaru",
                ViewModelToLoad = typeof(VoterHistoryViewModel),
                MenuItemType = MenuItemType.BreweryStats
            });

            return menuItems;
        }

        private void LoadDefaultMenuItems()
        {
            _userMenuItems.Add(_menuItems[MenuItemType.Logout]);
        }

        private void LoadAdminMenuItems()
        {
            _userMenuItems.Add(_menuItems[MenuItemType.AdminPanel]);
            _userMenuItems.Add(_menuItems[MenuItemType.Battle]);
            _userMenuItems.Add(_menuItems[MenuItemType.VoterHistory]);
            _userMenuItems.Add(_menuItems[MenuItemType.Logout]);
        }

        private void LoadVoterMenuItems()
        {
            _userMenuItems.Add(_menuItems[MenuItemType.Battle]);
            _userMenuItems.Add(_menuItems[MenuItemType.VoterHistory]);
            _userMenuItems.Add(_menuItems[MenuItemType.Logout]);
        }
        private void LoadBreweryStatsMenuItems()
        {
            _userMenuItems.Add(_menuItems[MenuItemType.BreweryStats]);
            _userMenuItems.Add(_menuItems[MenuItemType.Battle]);
            _userMenuItems.Add(_menuItems[MenuItemType.VoterHistory]);
            _userMenuItems.Add(_menuItems[MenuItemType.Logout]);
        }

        public string WelcomeText => "Hello " + _settingsService.UserNameSetting;

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _userMenuItems;
            set
            {
                _userMenuItems = value;
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
                MenuItemType = MenuItemType.Logout
            });
        }
    }
}
