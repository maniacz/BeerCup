using Acr.UserDialogs;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        void ShowToast(string message);

        Task<bool> Confirm(string message, string title, string okText, string cancelText);

        Task<PromptResult> ShowPrompt(string message, string title, string okText, string cancelText, string placeholder, InputType inputType);
    }
}
