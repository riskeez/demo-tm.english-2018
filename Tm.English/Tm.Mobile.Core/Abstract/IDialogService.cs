using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tm.Mobile.Core.Abstract
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);

        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);

        Task<bool> ShowYesNoQuestion(string question, string title);

        Task<string> ShowSheetDialog(IEnumerable<string> items, string title, string buttonCancelText);
    }
}
