using Acr.UserDialogs;
using BeerCup.Mobile.Contracts.Services.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
        public Task<bool> Confirm(string message, string title, string okText, string cancelText)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText);
        }
    }
}
