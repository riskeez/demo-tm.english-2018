using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Mobile.Core.Abstract;
using Xamarin.Forms;

namespace Tm.English.Services
{
    public class DialogService : IDialogService
    {
        private Page CurrentPage => Application.Current.MainPage;

        public async Task ShowMessage(string message, string title)
        {
            await CurrentPage.DisplayAlert(title, message, "Ok");
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var result = await CurrentPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);
            afterHideCallback?.Invoke(result);
            return result;
        }

        public async Task<string> ShowSheetDialog(IEnumerable<string> items, string title, string buttonCancelText)
        {
            var choiceResult = await CurrentPage.DisplayActionSheet(title, buttonCancelText, null, items.ToArray());

            if (choiceResult != buttonCancelText || (choiceResult == buttonCancelText && items.Contains(buttonCancelText)))
            {
                return choiceResult;
            }

            return string.Empty;
        }

        public Task<bool> ShowYesNoQuestion(string question, string title)
        {
            return ShowMessage(question, title, "Yes", "No", null);
        }
    }
}
