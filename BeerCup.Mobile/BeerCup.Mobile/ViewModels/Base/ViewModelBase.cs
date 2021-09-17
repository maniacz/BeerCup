using BeerCup.Mobile.Contracts.Services.General;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BeerCup.Mobile.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
